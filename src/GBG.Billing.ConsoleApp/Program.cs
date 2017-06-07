using System;
using System.Configuration;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Events;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Microsoft.ServiceBus;

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

            try
            {
                var bus = Bus.Factory.CreateUsingAzureServiceBus(sbc =>
                {
                    var serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", ConfigurationManager.AppSettings["AzureSbNamespace"], ConfigurationManager.AppSettings["AzureSbPath"]);

                    var host = sbc.Host(serviceUri, h =>
                    {
                        h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(ConfigurationManager.AppSettings["AzureSbKeyName"], ConfigurationManager.AppSettings["AzureSbSharedAccessKey"], TimeSpan.FromDays(1), TokenScope.Namespace);
                    });

                    sbc.ReceiveEndpoint(host, ConfigurationManager.AppSettings["ServiceQueueName"], e =>
                    {
                        // Configure your consumer(s)
                        e.Consumer<TextProcessedHandler>();
                        e.DefaultMessageTimeToLive = TimeSpan.FromMinutes(1);
                        e.EnableDeadLetteringOnMessageExpiration = false;
                    });
                });
                await bus.StartAsync();

                Console.WriteLine("Press any key to stop!");
                Console.ReadKey();
            }
            finally
            {
            }
        }
    }
}
