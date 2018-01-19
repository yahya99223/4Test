using Helpers.Core;
using MassTransit;
using Message.Contracts;

namespace OrderManagement
{
    public class BusHelper
    {
        public static IBusControl GetBusControl()
        {
            var mqUri = MessagingConstants.MqUri;
            var userName = MessagingConstants.UserName;
            var password = MessagingConstants.Password;
            var bus = BusConfigurator.ConfigureBus(mqUri, userName, password, (cfg, host) =>
            {
                //cfg.UseRetry(retryConfig => retryConfig.Interval(7, TimeSpan.FromSeconds(5)));

                cfg.ReceiveEndpoint(host, MessagingConstants.OrderManagementQueue, e =>
                {

                });
            });

            bus.Start();
            return bus;
        }
    }
}