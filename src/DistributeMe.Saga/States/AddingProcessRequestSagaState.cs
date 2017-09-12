using System;
using System.ComponentModel.DataAnnotations.Schema;
using Automatonymous;

namespace DistributeMe.Saga
{
    public class AddingProcessRequestSagaState : SagaStateMachineInstance
    {
        private CompositeEventStatus requestFinishedStatus;
        private int facesCount;
        private string extractedText;
        private Guid correlationId;

        public Guid CorrelationId
        {
            get { return correlationId; }
            set { correlationId = value; }
        }

        public Guid RequestId { get; set; }
        public string CurrentState { get; set; }

        public string ExtractedText
        {
            get { return extractedText; }
            set { extractedText = value; }
        }

        public DateTime StartProcessDate { get; set; }
        public DateTime? EndProcessDate { get; set; }

        public int FacesCount
        {
            get { return facesCount; }
            set { facesCount = value; }
        }

        public bool OcrFinished { get; set; }
        public bool FaceFinished { get; set; }
        public int RequestFinishedStatusBits { get; set; }

        public CompositeEventStatus RequestFinishedStatus
        {
            get { return new CompositeEventStatus(RequestFinishedStatusBits); }
            set { RequestFinishedStatusBits = value.Bits; }
        }
    }
}