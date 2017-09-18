using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenPipes;
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
                cfg.UseRetry(context => context.SetRetryPolicy(x => x.Interval(10, TimeSpan.FromSeconds(3))));
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
