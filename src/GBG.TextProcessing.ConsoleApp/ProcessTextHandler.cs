using System;
using System.Threading.Tasks;
using GBG.Microservices.Messaging.Commands;
using GBG.Microservices.Messaging.Events;
using MassTransit;

namespace GBG.TextProcessing.ConsoleApp
{
    public class ProcessTextHandler : IConsumer<ProcessTextCommand>
    {
        public async Task Consume(ConsumeContext<ProcessTextCommand> context)
        {
            Console.WriteLine($"We got message from: {context?.Message?.Sender}");

            var message = context.Message;

            await Task.Delay(7000);
            if (message.Sender.ToLower() == "wahid")
                throw new NotImplementedException();


            char[] charArray = message.Text.ToCharArray();
            Array.Reverse(charArray);
            var processedText = new string(charArray);

            Console.WriteLine($"Process result: {processedText}");

            await context.Publish<TextProcessed>(new TextProcessed
            {
                CreateDate = message.CreateDate,
                Sender = message.Sender,
                ProcessDate = DateTime.UtcNow,
                ProcessedText = processedText,
            });
        }
    }
}
