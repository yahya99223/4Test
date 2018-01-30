using System;
using System.Threading.Tasks;
using MassTransit;
using Message.Contracts;

namespace Saga.Service
{
/*    public class ViolationObserver : IConsumeMessageObserver<IValidatedMessage>
    {
        public async Task PreConsume(ConsumeContext<IValidatedMessage> context)
        {
            if (!context.Message.IsValid)
            {
                await context.NotifyConsumed(TimeSpan.FromSeconds(1), "ViolationObserver");
                await context.Publish<IViolationOccurredEvent>(new ViolationOccurredEvent(context.Message.CorrelationId, context.Message.Violations));
                throw new Exception("There is violations");
            }
        }

        public Task PostConsume(ConsumeContext<IValidatedMessage> context)
        {
            return Task.CompletedTask;
        }

        public Task ConsumeFault(ConsumeContext<IValidatedMessage> context, Exception exception)
        {
            return Task.CompletedTask;
        }
    }*/
}