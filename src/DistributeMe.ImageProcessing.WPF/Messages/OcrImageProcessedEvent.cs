using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.WPF.Messages
{
    public class OcrImageProcessedEvent : IOcrImageProcessedEvent
    {
        public Guid RequestId { get; set; }
        public string ExtractedText { get; set; }
        public DateTime ProcessStartTime { get; set; }
        public DateTime ProcessFinishTime { get; set; }
    }
}