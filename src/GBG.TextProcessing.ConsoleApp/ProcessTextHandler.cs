using System;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Commands;
using GBG.Microservices.Messaging.Events;

namespace GBG.TextProcessing.ConsoleApp
{
    public class ProcessTextHandler : IHandleMessages<ProcessTextCommand>
    {
        public async Task Handle(ProcessTextCommand message, IMessageHandlerContext context)
        {
            await Task.Delay(7000);
            if (message.Sender.ToLower() == "wahid")
                throw new NotImplementedException();


            char[] charArray = message.Text.ToCharArray();
            Array.Reverse(charArray);
            var processedText = new string(charArray);

            await context.Publish<ITextProcessed>(e =>
            {
                e.CreateDate = message.CreateDate;
                e.Sender = message.Sender;
                e.ProcessDate = DateTime.UtcNow;
                e.ProcessedText = processedText;
            });
        }
    }
}
