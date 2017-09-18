using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.Ocr
{
    internal class ProcessOcrConsumer : IConsumer<IProcessRequestAddedEvent>
    {
        private Random random;

        public ProcessOcrConsumer()
        {
            random = new Random();
        }

        public async Task Consume(ConsumeContext<IProcessRequestAddedEvent> context)
        {
            var command = context.Message;

            Console.WriteLine($"Processing Request: {command.RequestId}");

            var processStartDate = DateTime.UtcNow;
            await Task.Delay(random.Next(2000, 9000));

            Console.WriteLine($"FINISHED {command.RequestId}");

            var notificationEvent = new OcrImageProcessedEvent(command.RequestId, "extracted text", processStartDate, DateTime.UtcNow);
            await context.Publish<IOcrImageProcessedEvent>(notificationEvent);
        }
    }
}