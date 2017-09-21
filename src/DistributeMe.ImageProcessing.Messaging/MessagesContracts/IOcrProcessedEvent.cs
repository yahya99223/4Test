using System;

namespace DistributeMe.ImageProcessing.Messaging
{
    public interface IOcrProcessedEvent
    {
        Guid RequestId { get; }
        Guid CorrelationId { get; set; }
        string ExtractedText { get; }
        DateTime ProcessStartTime { get; }
        DateTime ProcessFinishTime { get; }
    }

    public class OcrProcessedEvent : IOcrProcessedEvent
    {
        public OcrProcessedEvent(Guid requestId, string extractedText, DateTime processStartTime, DateTime processFinishTime)
        {
            RequestId = requestId;
            ExtractedText = extractedText;
            ProcessStartTime = processStartTime;
            ProcessFinishTime = processFinishTime;
        }

        public Guid RequestId { get; }
        public Guid CorrelationId { get; set; }
        public string ExtractedText { get; }
        public DateTime ProcessStartTime { get; }
        public DateTime ProcessFinishTime { get; }
    }
}
