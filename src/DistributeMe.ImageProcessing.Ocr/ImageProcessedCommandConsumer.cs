using System;
using System.Text;
using System.Threading;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.Ocr.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DistributeMe.ImageProcessing.Ocr
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
            Console.WriteLine($"OCR engine processing the Image in request:{commandObj.RequestId}");
            Thread.Sleep(4500);
            Console.WriteLine("DONE.");

            var ocrImageProcessedEvent = new OcrImageProcessedEvent(commandObj.RequestId, "extracted text", startDate, DateTime.UtcNow);
            rabbitMqManager.SendOcrImageProcessedEvent(ocrImageProcessedEvent);
        }
    }
}