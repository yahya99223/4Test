using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.FaceRecognition.Messages
{
    public class FaceRecognitionImageProcessedEvent : IFaceRecognitionImageProcessedEvent
    {
        public FaceRecognitionImageProcessedEvent(Guid requestId, int facesCount, DateTime processStartTime, DateTime processFinishTime)
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