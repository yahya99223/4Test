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

            await context.Publish(new NormalizeOrderResponse
            {
                OrderId = command.OrderId,
                StartProcessTime = startTime,
                NormalizedText = result,
                EndProcessTime = DateTime.UtcNow,
            });
        }
    }
}