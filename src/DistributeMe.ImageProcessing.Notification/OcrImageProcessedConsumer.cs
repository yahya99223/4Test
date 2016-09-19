using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.Notification
{
    public class OcrImageProcessedConsumer
    {
        public void Consume(IOcrImageProcessedEvent registeredEvent)
        {
            Console.WriteLine($"Customer notification sent: OCR result for Request id {registeredEvent.RequestId} ready");
        }
    }
}