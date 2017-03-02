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
                cfg.ReceiveEndpoint(host, MessagingConstants.MessageProcessQueue, e =>
                {
                    e.Consumer(() => new ReceiveProcessedLetter());
                });
            });

            Bus.Start();
        }
    }
}