using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Core;
using MassTransit;
using MassTransit.Events;
using Message.Contracts;

namespace Validate.Service
{
    public class ValidateOrderCommandConsumer : IConsumer<IValidateOrderCommand>
    {
        private Random random;

        public ValidateOrderCommandConsumer()
        {
            random = new Random();
        }

        public async Task Consume(ConsumeContext<IValidateOrderCommand> context)
        {
            var violationHandler = new ViolationHandler<IValidateOrderCommand>();
            var command = context.Message;

            try
            {
                if (random.Next(1, 9) % 2 == 0)
                    throw new Exception("Bad luck!. Try again :P");

                if (command == null)
                    throw new InternalApplicationException<IValidateOrderCommand>(x => x, ViolationType.Null);

                if (string.IsNullOrWhiteSpace(command.OriginalText))
                    throw new InternalApplicationException<IValidateOrderCommand>(x => x.OriginalText, ViolationType.Required);

                if (command.OriginalText.Contains("asd"))
                    violationHandler.AddViolation(x => x.OriginalText, ViolationType.NotAllowed);

                if (command.OriginalText.Contains("123"))
                    violationHandler.AddViolation(x => x.OriginalText, ViolationType.Invalid);

                if (!violationHandler.IsValid)
                    throw new InternalApplicationException(violationHandler.Violations);

                await context.Publish<IValidateOrderResponse>(new ValidateOrderResponse(command.OrderId)
                {
                    StartProcessTime = DateTime.UtcNow,
                    EndProcessTime = DateTime.UtcNow,
                });
            }
            catch (InternalApplicationException ex)
            {
                await context.Publish<IViolationOccurredEvent>(new ViolationOccurredEvent(command.OrderId, "Validation", "The Order is not valid", ex.Violations));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}