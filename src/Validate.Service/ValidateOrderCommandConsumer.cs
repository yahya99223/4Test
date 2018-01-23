using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Core;
using MassTransit;
using Message.Contracts;

namespace Validate.Service
{
    public class ValidateOrderCommandConsumer : IConsumer<IValidateOrderCommand>
    {
        public async Task Consume(ConsumeContext<IValidateOrderCommand> context)
        {
            var violationHandler = new ViolationHandler<IValidateOrderCommand>();
            var command = context.Message;

            try
            {
                if (command == null)
                    throw new NullReferenceException("There is no message in the context");

                if (string.IsNullOrWhiteSpace(command.OriginalText))
                    throw new InternalApplicationException<IValidateOrderCommand>(x => x.OriginalText, ViolationType.Required);

                if (command.OriginalText.Contains("asd"))
                    violationHandler.AddViolation(x => x.OriginalText, ViolationType.NotAllowed);

                if (command.OriginalText.Contains("123"))
                    violationHandler.AddViolation(x => x.OriginalText, ViolationType.Invalid);
            }
            catch (InternalApplicationException ex)
            {
                violationHandler.AddRange(ex.Violations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            await context.Publish<IValidateOrderResponse>(new ValidateOrderResponse(violationHandler.Violations)
            {
                OrderId = command.OrderId,
                StartProcessTime = DateTime.UtcNow,
                EndProcessTime = DateTime.UtcNow,
                //Errors = errors
            });
        }
    }
}