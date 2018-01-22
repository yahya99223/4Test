using System;
using System.Threading.Tasks;
using MassTransit;
using Message.Contracts;

namespace Capitalize.Service
{
    public class CapitalizeOrderCommandConsumer : IConsumer<ICapitalizeOrderCommand>
    {
        public async Task Consume(ConsumeContext<ICapitalizeOrderCommand> context)
        {
            var startTime = DateTime.UtcNow;
            var command = context.Message;
            var result = command.OriginalText.ToUpper();

            await context.Publish<ICapitalizeOrderResponse>(new CapitalizeOrderResponse
            {
                OrderId = command.OrderId,
                StartProcessTime = startTime,
                CapitalizeText = result,
                EndProcessTime = DateTime.UtcNow,
            });
        }
    }
}