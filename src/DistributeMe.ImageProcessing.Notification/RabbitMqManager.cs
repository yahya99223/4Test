using System;
using System.Text;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.Notification.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DistributeMe.ImageProcessing.Notification
{
    public class RabbitMqManager : IDisposable
    {
        private IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = new ConnectionFactory {Uri = MessagingConstants.MqUri};
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForFaceProcessImageEvent()
        {
            channel.QueueDeclare(
                queue: MessagingConstants.ProcessedFaceNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var eventingConsumer = new EventingBasicConsumer(channel);
            eventingConsumer.Received += (chan, eventArgs) =>
            {
                var contentType = eventArgs.BasicProperties.ContentType;
                if (contentType != MessagingConstants.ContentType)
                    throw new ArgumentException($"Can't handle content type {contentType}");

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                var orderConsumer = new FaceRecognitionImageProcessedConsumer();
                var commandObj =
                JsonConvert.DeserializeObject<FaceRecognitionImageProcessedEvent>(message);
                orderConsumer.Consume(commandObj);
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag,
                    multiple: false);
            };

            channel.BasicConsume(
                queue: MessagingConstants.ProcessedFaceNotificationQueue,
                noAck: false,
                consumer: eventingConsumer);
        }
        public void ListenForOcrProcessImageEvent()
        {
            channel.QueueDeclare(
                queue: MessagingConstants.ProcessedOcrNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);
            
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var eventingConsumer = new EventingBasicConsumer(channel);
            eventingConsumer.Received += (chan, eventArgs) =>
            {
                var contentType = eventArgs.BasicProperties.ContentType;
                if (contentType != MessagingConstants.ContentType)
                    throw new ArgumentException($"Can't handle content type {contentType}");

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                var orderConsumer = new OcrImageProcessedConsumer();
                var commandObj =
                JsonConvert.DeserializeObject<OcrImageProcessedEvent>(message);
                orderConsumer.Consume(commandObj);
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag,
                    multiple: false);
            };

            channel.BasicConsume(
                queue: MessagingConstants.ProcessedOcrNotificationQueue,
                noAck: false,
                consumer: eventingConsumer);
        }
        
        
        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
