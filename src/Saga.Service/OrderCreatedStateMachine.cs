using System;
using Automatonymous;
using Message.Contracts.Events;

namespace Saga.Service
{
    public class OrderCreatedStateMachine : MassTransitStateMachine<OrderCreatedSagaState>
    {
        public OrderCreatedStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => OrderCreated, x => x.CorrelateBy(cart => cart.OrderId.ToString(), context => context.Message.OrderId.ToString())
                .SelectId(context => Guid.NewGuid()));

            Event(() => OrderProcessRequested, x => x.CorrelateById(context => context.Message.OrderId));
            Initially(When(OrderCreated)
                .Then(context =>
                {
                    context.Instance.OriginalText = context.Data.OriginalText;
                    context.Instance.CreateDate = context.Data.CreateDate;
                })
                .TransitionTo(Active)
            );
        }

        public State Active { get; private set; }        
        public Event<IOrderCreated> OrderCreated { get; set; }
        public Event<IOrderProcessRequested> OrderProcessRequested { get; set; }

        //public State ProcessingFinished { get; private set; }
    }
}