using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Shared.Messaging.Events;


namespace Producer.WebAPI.MessagingHandlers
{
    public class ProcessNewLetter : IConsumer<NewLetterReceived>
    {
        public async Task Consume(ConsumeContext<NewLetterReceived> context)
        {
            var startTime = DateTime.UtcNow;
            await Task.Delay(1300);
            var updatedBody = new string(context.Message.Body.Reverse().ToArray());

            var letterProcessedEvent = new LetterProcessed()
            {
                Id = context.Message.Id,
                ProcessStartDate = startTime,
                ProcessEndDate = DateTime.UtcNow,
                UpdatedBody = updatedBody,
            };

            await context.Publish(letterProcessedEvent);
        }
    }
}