using System;
using System.Threading;
using System.Threading.Tasks;
using GBG.Microservices.Messaging;
using GBG.Microservices.Messaging.Commands;
using GBG.Microservices.Messaging.Events;
using MassTransit;
using Microsoft.ServiceBus.Messaging;

namespace GBG.TextProcessing.ConsoleApp
{
    public class ProcessTextNativeHandler : IHandler
    {
        public void Handle(BrokeredMessage brokeredMessage)
        {
            var message = brokeredMessage.GetBody<ProcessTextCommand>();

            Console.WriteLine($"We got message from: {message?.Sender}");

            if (message?.Sender.ToLower() == "wahid")
                throw new NotImplementedException();

            if (!string.IsNullOrEmpty(message?.Text))
            {
                char[] charArray = message.Text.ToCharArray();
                Array.Reverse(charArray);
                var processedText = new string(charArray);

                Console.WriteLine($"Process result: {processedText}");
            }
        }
    }

    public class ProcessTextHandler : IConsumer<ProcessTextCommand>
    {
        public async Task Consume(ConsumeContext<ProcessTextCommand> context)
        {
            Console.WriteLine($"We got message from: {context?.Message?.Sender}");

            var message = context?.Message;

            if (message?.Sender.ToLower() == "wahid")
                throw new NotImplementedException();

            if (!string.IsNullOrEmpty(message?.Text))
            {
                char[] charArray = message.Text.ToCharArray();
                Array.Reverse(charArray);
                var processedText = new string(charArray);

                Console.WriteLine($"Process result: {processedText}");

                await context.Publish(new TextProcessed
                {
                    CreateDate = message.CreateDate,
                    Sender = message.Sender,
                    ProcessDate = DateTime.UtcNow,
                    ProcessedText = processedText,
                });
            }
        }
    }
}
