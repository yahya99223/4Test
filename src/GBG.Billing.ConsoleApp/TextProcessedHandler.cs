using System;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Events;

namespace GBG.Billing.ConsoleApp
{
    public class TextProcessedHandler : IHandleMessages<ITextProcessed>
    {
        private static int coast = 0;
        public Task Handle(ITextProcessed message, IMessageHandlerContext context)
        {
            coast += message.ProcessedText.Length;
            Console.WriteLine(coast);

            return Task.FromResult<object>(null);
        }
    }
}
