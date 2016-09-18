using System;
using System.Text;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.Ocr.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DistributeMe.ImageProcessing.Ocr
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
                queue: MessagingConstants.ProcessOcrQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new ImageProcessedCommandConsumer(this);

            channel.BasicConsume(queue: MessagingConstants.ProcessOcrQueue, noAck: false, consumer: consumer);
        }

        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void SendOcrImageProcessedEvent(OcrImageProcessedEvent ocrImageProcessedEvent)
        {
            channel.ExchangeDeclare(
                exchange: MessagingConstants.ProcessedOcrExchange,
                type: ExchangeType.Fanout);
            channel.QueueDeclare(
                queue: MessagingConstants.ProcessedOcrNotificationQueue,
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);
            channel.QueueBind(
                queue: MessagingConstants.ProcessedOcrNotificationQueue,
                exchange: MessagingConstants.ProcessedOcrExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(ocrImageProcessedEvent);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = MessagingConstants.ContentType;

            channel.BasicPublish(
                exchange: MessagingConstants.ProcessedOcrExchange,
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
