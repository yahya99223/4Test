using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Core;
using MassTransit;
using Message.Contracts;

namespace Capitalize.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = BusConfigurator.ConfigureBus(MessagingConstants.MqUri, MessagingConstants.UserName, MessagingConstants.Password, (cfg, host) =>
            {
                //cfg.UseRetry(retryConfig => retryConfig.Interval(7, TimeSpan.FromSeconds(5)));

                cfg.ReceiveEndpoint(host, MessagingConstants.CapitalizeServiceQueue, e =>
                {
                    e.Consumer<CapitalizeOrderCommandConsumer>();
                });
            });

            bus.Start();
        }
    }
}
