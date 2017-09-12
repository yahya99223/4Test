using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IFaceRecognitionImageProcessedEvent
    {
        Guid RequestId { get; }
        Guid CorrelationId { get; set; }
        int FacesCount { get; }
        DateTime ProcessStartTime { get; }
        DateTime ProcessFinishTime { get; }
    }
}