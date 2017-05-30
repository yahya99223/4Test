using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Events;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Unicast.Subscriptions;

namespace GBG.Billing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Billing Console";
            LogManager.Use<DefaultFactory>().Level(LogLevel.Info);

            var endpointConfiguration = new EndpointConfiguration("Billing.ConsoleApp");
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

            var routing = endpointConfiguration.UseTransport<MsmqTransport>().Routing();
            routing.RegisterPublisher(typeof(ITextProcessed), "TextProcessing.ConsoleApp");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            
            try
            {
                Console.WriteLine("Press any key to stop!");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Stop().ConfigureAwait(false);
            }
        }
    }
}
