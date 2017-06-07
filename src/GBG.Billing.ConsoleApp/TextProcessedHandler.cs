using System;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Events;
using MassTransit;

namespace GBG.Billing.ConsoleApp
{
    public class TextProcessedHandler : IConsumer<TextProcessed>
    {
        private static int coast = 0;


        public Task Consume(ConsumeContext<TextProcessed> context)
        {
            coast += context?.Message?.ProcessedText?.Length ?? 0;
            Console.WriteLine(coast);

            return Task.FromResult<object>(null);
        }
    }
}
