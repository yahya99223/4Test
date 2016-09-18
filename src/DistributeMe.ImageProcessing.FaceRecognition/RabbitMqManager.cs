using System;
using System.Text;
using DistributeMe.ImageProcessing.FaceRecognition.Messages;
using DistributeMe.ImageProcessing.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DistributeMe.ImageProcessing.FaceRecognition
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

        public void ListenForProcessImageCommand()
        {
            channel.QueueDeclare(
                queue: MessagingConstants.ProcessFaceQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new ImageProcessedCommandConsumer(this);

            channel.BasicConsume(queue: MessagingConstants.ProcessFaceQueue, noAck: false, consumer: consumer);
        }

        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void SendFaceImageProcessedEvent(FaceRecognitionImageProcessedEvent faceImageProcessedEvent)
        {
            channel.ExchangeDeclare(
                exchange: MessagingConstants.ProcessedFaceExchange,
                type: ExchangeType.Fanout);
            channel.QueueDeclare(
                queue: MessagingConstants.ProcessedFaceNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);
            channel.QueueBind(
                queue: MessagingConstants.ProcessedFaceNotificationQueue,
                exchange: MessagingConstants.ProcessedFaceExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(faceImageProcessedEvent);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = MessagingConstants.ContentType;

            channel.BasicPublish(
                exchange: MessagingConstants.ProcessedFaceExchange,
                routingKey: "",
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
