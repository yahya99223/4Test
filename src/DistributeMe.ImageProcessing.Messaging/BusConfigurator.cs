using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DistributeMe.ImageProcessing.Messaging
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(MessagingConstants.MqUri), hst =>
                {
                    hst.Username(MessagingConstants.UserName);
                    hst.Password(MessagingConstants.Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
