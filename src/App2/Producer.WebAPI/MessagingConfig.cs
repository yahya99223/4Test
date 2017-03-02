using MassTransit;
using Producer.WebAPI.MessagingHandlers;
using Shared.Messaging;


namespace Producer.WebAPI
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
                    e.Consumer(() => new ProcessNewLetter());
                });
            });

            Bus.Start();
        }
    }
}