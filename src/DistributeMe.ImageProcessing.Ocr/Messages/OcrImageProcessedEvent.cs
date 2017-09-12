using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.Ocr.Messages
{
    public class OcrImageProcessedEvent : IOcrImageProcessedEvent
    {
        public OcrImageProcessedEvent(Guid requestId, string extractedText, DateTime processStartTime, DateTime processFinishTime)
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