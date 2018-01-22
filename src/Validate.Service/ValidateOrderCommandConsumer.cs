using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Message.Contracts;

namespace Validate.Service
{
    public class ValidateOrderCommandConsumer : IConsumer<IValidateOrderCommand>
    {
        public async Task Consume(ConsumeContext<IValidateOrderCommand> context)
        {
            var command = context.Message;

            await context.Publish<IValidateOrderResponse>(new ValidateOrderResponse
            {
                OrderId = command.OrderId,
                StartProcessTime = DateTime.UtcNow,
                EndProcessTime = DateTime.UtcNow,
                IsValid = true,
            });
        }
    }
}
