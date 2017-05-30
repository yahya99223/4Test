using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace GBG.TextProcessing.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "TextProcessing Console";
            LogManager.Use<DefaultFactory>().Level(LogLevel.Info);

            var endpointConfiguration = new EndpointConfiguration("TextProcessing.ConsoleApp");
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

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
