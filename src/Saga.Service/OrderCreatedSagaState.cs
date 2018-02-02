using System;
using System.Collections.Generic;
using Automatonymous;

namespace Saga.Service
{
    public class OrderCreatedSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public Guid OrderId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public string RemainingServices { get; set; }

        public int RequestFinishedStatusBits { get; set; }

        public CompositeEventStatus RequestFinishedStatus
        {
            get { return new CompositeEventStatus(RequestFinishedStatusBits); }
            set { RequestFinishedStatusBits = value.Bits; }
        }

    }
}