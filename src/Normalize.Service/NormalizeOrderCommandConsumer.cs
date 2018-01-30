using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MassTransit;
using Message.Contracts;

namespace Normalize.Service
{
    public class NormalizeOrderCommandConsumer : IConsumer<INormalizeOrderCommand>
    {
        public async Task Consume(ConsumeContext<INormalizeOrderCommand> context)
        {
            var startTime = DateTime.UtcNow;
            var command = context.Message;
            var result = Regex.Replace(command.OriginalText, " {2,}", " ");
            Console.WriteLine($"Processing the message: {command.OriginalText} from the order: {command.OrderId}");
            Console.WriteLine($"The result is: {result}");

            await context.Publish<INormalizeOrderResponse>(new NormalizeOrderResponse
            {
                OrderId = command.OrderId,
                StartProcessTime = startTime,
                NormalizedText = result,
                EndProcessTime = DateTime.UtcNow,
            });
        }
    }
}