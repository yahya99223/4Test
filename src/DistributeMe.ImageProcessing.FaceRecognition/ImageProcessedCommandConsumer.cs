using System;
using System.Text;
using System.Threading;
using DistributeMe.ImageProcessing.FaceRecognition.Messages;
using DistributeMe.ImageProcessing.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DistributeMe.ImageProcessing.FaceRecognition
{
    public class ImageProcessedCommandConsumer : DefaultBasicConsumer
    {
        private readonly RabbitMqManager rabbitMqManager;

        public ImageProcessedCommandConsumer(RabbitMqManager rabbitMqManager)
        {
            this.rabbitMqManager = rabbitMqManager;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {

            if (properties.ContentType != MessagingConstants.ContentType)
                throw new ArgumentException($"Can't handle content type {properties.ContentType}");

            var message = Encoding.UTF8.GetString(body);

            var commandObj = JsonConvert.DeserializeObject<ProcessImageCommand>(message);
            consume(commandObj);
            rabbitMqManager.SendAck(deliveryTag);
        }

        private void consume(ProcessImageCommand commandObj)
        {
            var startDate = DateTime.UtcNow;
            Console.WriteLine($"FaceRecognition engine processing the Image in request:{commandObj.RequestId}");
            Thread.Sleep(1200);

            var ocrImageProcessedEvent = new FaceRecognitionImageProcessedEvent(commandObj.RequestId, 2, startDate, DateTime.UtcNow);
            rabbitMqManager.SendFaceImageProcessedEvent(ocrImageProcessedEvent);
        }
    }
}