using System;
using DistributeMe.ImageProcessing.Messaging;

namespace DistributeMe.ImageProcessing.Notification
{
    public class FaceRecognitionImageProcessedConsumer
    {
        public void Consume(IFaceRecognitionImageProcessedEvent registeredEvent)
        {
            Console.WriteLine($"Customer notification sent: Face Recognition result for Request id {registeredEvent.RequestId} ready");
        }
    }
}