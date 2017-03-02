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
            context.Message.Letter.Body = new string(context.Message.Letter.Body.Reverse().ToArray());

            var letterProcessedEvent = new LetterProcessed(context.Message.Letter);
            await context.Publish(letterProcessedEvent);
        }
    }
}