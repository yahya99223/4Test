using System;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Helpers.Core
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(string mqUri, string userName, string password, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(mqUri), h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });
                registrationAction?.Invoke(cfg, host);
            });
        }

    }
}