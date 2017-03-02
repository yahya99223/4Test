using Consumer.WebAPI.MessagingHandlers;
using MassTransit;
using Shared.Messaging;


namespace Consumer.WebAPI
{
    public static class MessagingConfig
    {
        public static IBusControl Bus { get; private set; }


        public static void SetupBus()
        {
            Bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, MessagingConstants.MessageManagingQueue, e =>
                {
                    e.Consumer(() => new ReceiveProcessedLetter());
                });
            });

            Bus.Start();
        }
    }
}