using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IFaceRecognitionImageProcessedEvent
    {
        Guid RequestId { get; }
        int FacesCount { get; }
        DateTime ProcessStartTime { get; }
        DateTime ProcessFinishTime { get; }
    }
}