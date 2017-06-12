using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace GBG.Microservices.Messaging
{
    public interface IHandler
    {
        void Handle(BrokeredMessage message);
    }

    public interface IBusWrapper
    {
        Task SendAsync<T>(T message, string destination);
        void Send<T>(T message, string destination);

        Task PublishAsync<T>(T message, string topic);
        void Publish<T>(T message, string topic);

        void Subscribe(string topic);
    }

    public interface IHandler<in T>
    {
        void Handle(T message);
    }
}
