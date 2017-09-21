using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IFaceProcessedEvent
    {
        Guid RequestId { get; }
        Guid CorrelationId { get; set; }
        int FacesCount { get; }
        DateTime ProcessStartTime { get; }
        DateTime ProcessFinishTime { get; }
    }

    public class FaceProcessedEvent : IFaceProcessedEvent
    {
        public FaceProcessedEvent(Guid requestId, int facesCount, DateTime processStartTime, DateTime processFinishTime)
        {
            RequestId = requestId;
            FacesCount = facesCount;
            ProcessStartTime = processStartTime;
            ProcessFinishTime = processFinishTime;
        }

        public Guid RequestId { get; }
        public Guid CorrelationId { get; set; }
        public int FacesCount { get; }
        public DateTime ProcessStartTime { get; }
        public DateTime ProcessFinishTime { get; }
    }
}