using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.Notification.Messages
{
    public class FaceRecognitionImageProcessedEvent : IFaceRecognitionImageProcessedEvent
    {
        public Guid RequestId { get; set; }
        public int FacesCount { get; set; }
        public DateTime ProcessStartTime { get; set; }
        public DateTime ProcessFinishTime { get; set; }
    }
}