using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IOcrImageProcessedEvent
    {
        Guid RequestId { get; }
        Guid CorrelationId { get; set; }
        string ExtractedText { get; }
        DateTime ProcessStartTime { get; }
        DateTime ProcessFinishTime { get; }
    }
}
