using System;
using Automatonymous;

namespace Saga.Service
{
    public class OrderCreatedSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public State CurrentState { get; set; }
        public Guid OrderId { get; set; }
        public string OriginalText { get; set; }
        public DateTime CreateDate { get; set; }
    }
}