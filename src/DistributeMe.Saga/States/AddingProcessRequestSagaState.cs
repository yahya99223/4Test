using System;
using System.ComponentModel.DataAnnotations.Schema;
using Automatonymous;

namespace DistributeMe.Saga
{
    public class AddingProcessRequestSagaState : SagaStateMachineInstance
    {
        private CompositeEventStatus requestFinishedStatus;

        public Guid CorrelationId { get; set; }
        public Guid RequestId { get; set; }
        public string CurrentState { get; set; }
        public DateTime StartProcessDate { get; set; }
        public DateTime? EndProcessDate { get; set; }
        public int RequestFinishedStatusBits { get; set; }

        public CompositeEventStatus RequestFinishedStatus
        {
            get { return new CompositeEventStatus(RequestFinishedStatusBits); }
            set { RequestFinishedStatusBits = value.Bits; }
        }
    }
}