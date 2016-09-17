using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DistributeMe.ImageProcessing.WPF
{
    public class RabbitMqManager : IDisposable
    {
        private IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = MessagingConstants.MqUri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void SendProcessImageCommand(IProcessImageCommand command)
        {
            channel.ExchangeDeclare(
                exchange: MessagingConstants.ProcessImageExchange,
                type: ExchangeType.Direct);


            channel.QueueDeclare(
                queue: MessagingConstants.ProcessFaceQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            channel.QueueDeclare(
                queue: MessagingConstants.ProcessOcrQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind(
                queue: MessagingConstants.ProcessFaceQueue,
                exchange: MessagingConstants.ProcessImageExchange,
                routingKey: "");

            channel.QueueBind(
                queue: MessagingConstants.ProcessOcrQueue,
                exchange: MessagingConstants.ProcessImageExchange,
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = MessagingConstants.ContentType;

            channel.BasicPublish(
                exchange: MessagingConstants.ProcessImageExchange,
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
