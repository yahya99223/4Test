using System;
using System.Linq;
using Automatonymous;
using Message.Contracts;

namespace Saga.Service
{
    public class OrderCreatedStateMachine : MassTransitStateMachine<OrderCreatedSagaState>
    {
        public OrderCreatedStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreated, x => x.CorrelateBy(cart => cart.OrderId.ToString(), context => context.Message.OrderId.ToString())
                .SelectId(context => Guid.NewGuid()));

            Event(() => ValidateOrderResponse, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => NormalizeOrderResponse, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => CapitalizeOrderResponse, x => x.CorrelateById(context => context.Message.OrderId));

            //CompositeEvent(() => ProcessFinished, x => x.RequestFinishedStatus, NormalizeOrderResponse, CapitalizeOrderResponse);


            Initially(When(OrderCreated)
                .Then(context =>
                {
                    context.Instance.OriginalText = context.Data.OriginalText;
                    context.Instance.CreateDate = context.Data.CreateDate;
                    context.Instance.RemainingServices = string.Join("|", context.Data.Services);
                })
                .TransitionTo(Active)
                .If(context => context.Data.Services.Any(s => s == "Validate"), binder =>
                    binder.Publish(args => new ValidateOrderCommand
                    {
                        OriginalText = args.Data.OriginalText,
                        OrderId = args.Data.OrderId,                        
                    }))
                .If(context => context.Data.Services.All(s => s != "Validate"), binder =>

                    binder.TransitionTo(NoValidationRequired)

                        .If(c => c.Data.Services.Any(s => s == "Normalize"), builder =>
                            builder.Publish(args => new NormalizeOrderCommand
                            {
                                OriginalText = args.Data.OriginalText,
                                OrderId = args.Data.OrderId,
                            }))

                        .If(c => c.Data.Services.Any(s => s == "Capitalize"), builder =>
                            builder.Publish(args => new CapitalizeOrderCommand
                            {
                                OriginalText = args.Data.OriginalText,
                                OrderId = args.Data.OrderId,
                            }))
                )
            );

            During(Active,
                When(ValidateOrderResponse)
                    .Then(context =>
                    {
                        context.Instance.RemainingServices = string.Join("|", context.Instance.RemainingServices.Split('|').Where(s => s != "Validate"));
                    })
                    .TransitionTo(Validated)
                    .Publish(context => new OrderValidatedEvent
                    {
                        OrderId = context.Data.OrderId,
                        Errors = context.Data.Errors,
                        ProcessTime = (context.Data.EndProcessTime - context.Data.StartProcessTime).Milliseconds,
                    })
            );

            During(Validated, NoValidationRequired,
                When(NormalizeOrderResponse)
                    .Then(context =>
                    {
                        context.Instance.RemainingServices = string.Join("|", context.Instance.RemainingServices.Split('|').Where(s=>s!= "Normalize"));
                    })
                    .Publish(context => new OrderNormalized
                    {
                        OrderId = context.Data.OrderId,
                        NormalizedText = context.Data.NormalizedText,
                        ProcessTime = (context.Data.EndProcessTime - context.Data.StartProcessTime).Milliseconds,
                    }),
                When(CapitalizeOrderResponse)
                    .Then(context =>
                    {
                        context.Instance.RemainingServices = string.Join("|", context.Instance.RemainingServices.Split('|').Where(s => s != "Capitalize"));
                    })
                    .Publish(context => new OrderCapitalized
                    {
                        OrderId = context.Data.OrderId,
                        CapitalizedText = context.Data.CapitalizeText,
                        ProcessTime = (context.Data.EndProcessTime - context.Data.StartProcessTime).Milliseconds,
                    }),
                When(NormalizeOrderResponse, context => !context.Instance.RemainingServices.Any())
                    .TransitionTo(Finished)
                    .Finalize(),
                When(CapitalizeOrderResponse, context => !context.Instance.RemainingServices.Any())
                    .TransitionTo(Finished)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }


        public State Active { get; private set; }
        public State NoValidationRequired { get; private set; }
        public State Validated { get; private set; }
        public State Finished { get; private set; }


        public Event<IOrderCreatedEvent> OrderCreated { get; set; }
        public Event<IValidateOrderResponse> ValidateOrderResponse { get; set; }
        public Event<INormalizeOrderResponse> NormalizeOrderResponse { get; set; }
        public Event<ICapitalizeOrderResponse> CapitalizeOrderResponse { get; set; }

        //public Event ProcessFinished { get; set; }
    }
}