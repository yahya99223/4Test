﻿using System.Linq;
using Automatonymous;
using MassTransit;
using Message.Contracts;

namespace Saga.Service
{
    public class OrderCreatedStateMachine : MassTransitStateMachine<OrderCreatedSagaState>
    {
        public OrderCreatedStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreated, x => x.CorrelateBy(cart => cart.OrderId.ToString(), context => context.Message.OrderId.ToString())
                .SelectId(context => context.Message.OrderId/*Guid.NewGuid()*/));
            
            Event(() => ValidateOrderResponse, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => NormalizeOrderResponse, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => CapitalizeOrderResponse, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderReadyToProcessEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => ValidateOrderCommandFailed, x => x.CorrelateById(context => context.Message.Message.OrderId));
            //Event(() => ViolationOccurredEvent, x => x.CorrelateById(context => context.Message.CorrelationId));


            Initially(When(OrderCreated)
                .Then(context =>
                {
                    context.Instance.OrderId = context.Data.OrderId;
                    context.Instance.Text = context.Data.OriginalText;
                    context.Instance.CreateDate = context.Data.CreateDate;
                    context.Instance.RemainingServices = string.Join("|", context.Data.Services);
                })
                .TransitionTo(Active)
                .If(context => context.Data.Services.Any(s => s == "Validate"), binder => binder
                    .Publish(args => new ValidateOrderCommand
                    {
                        OriginalText = args.Data.OriginalText,
                        OrderId = args.Data.OrderId,
                    }))
                .If(context => context.Data.Services.All(s => s != "Validate"), binder => binder
                    .TransitionTo(NoValidationRequired)
                    .Publish(context => new OrderReadyToProcessEvent
                    {
                        OrderId = context.Data.OrderId,
                        OriginalText = context.Data.OriginalText,
                        Services = context.Data.Services,
                    })
                )
            );

            During(Active,
                When(ValidateOrderCommandFailed)
                    .Then(context =>
                    {
                        context.Instance.RemainingServices = "";
                    })
                    .TransitionTo(Finished)
                    .Finalize()
            );

            During(Active,
                When(ValidateOrderResponse)
                    .Then(context =>
                    {
                        context.Instance.RemainingServices = string.Join("|", context.Instance.RemainingServices.Split('|').Where(s => s != "Validate"));
                    })
                    .TransitionTo(Validated)
                    .Publish(context => new OrderReadyToProcessEvent
                    {
                        OrderId = context.Data.OrderId,
                        OriginalText = context.Instance.Text,
                        Services = context.Instance.RemainingServices.Split('|'),
                    })
                    .Publish(context => new OrderValidatedEvent
                    {
                        OrderId = context.Data.OrderId,
                        ProcessTime = (context.Data.EndProcessTime - context.Data.StartProcessTime).Milliseconds,
                    })
            );


            During(Validated, NoValidationRequired,
                When(OrderReadyToProcessEvent)
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
            );


            During(Validated, NoValidationRequired,
                When(NormalizeOrderResponse)
                    .Then(context =>
                    {
                        context.Instance.RemainingServices = string.Join("|", context.Instance.RemainingServices.Split('|').Where(s => s != "Normalize"));
                        context.Instance.Text = context.Data.NormalizedText;
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
                        context.Instance.Text = context.Data.CapitalizeText;                        
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
        public Event<IOrderReadyToProcessEvent> OrderReadyToProcessEvent { get; set; }
        public Event<IValidateOrderResponse> ValidateOrderResponse { get; set; }
        
        public Event<Fault<IValidateOrderCommand>> ValidateOrderCommandFailed { get; set; }

        //public Event<IViolationOccurredEvent> ViolationOccurredEvent { get; set; }
        public Event<INormalizeOrderResponse> NormalizeOrderResponse { get; set; }
        public Event<ICapitalizeOrderResponse> CapitalizeOrderResponse { get; set; }
    }
}