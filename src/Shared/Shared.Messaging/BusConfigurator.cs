using System;
using MassTransit;
using MassTransit.RabbitMqTransport;


namespace Shared.Messaging
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(MessagingConstants.MqUri), h =>
                {
                    h.Username(MessagingConstants.UserName);
                    h.Password(MessagingConstants.Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}