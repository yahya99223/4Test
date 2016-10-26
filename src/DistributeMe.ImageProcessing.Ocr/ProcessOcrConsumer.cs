using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.Ocr.Messages;
using MassTransit;

namespace DistributeMe.ImageProcessing.Ocr
{
    internal class ProcessOcrConsumer : IConsumer<IProcessImageCommand>
    {
        private Random random;

        public ProcessOcrConsumer()
        {
            random = new Random();
        }

        public async Task Consume(ConsumeContext<IProcessImageCommand> context)
        {
            var command = context.Message;

            await Console.Out.WriteLineAsync($"Processing Request: {command.RequestId}");

            var processStartDate = DateTime.UtcNow;
            Thread.Sleep(random.Next(1000, 5000));
            /*if (DateTime.UtcNow < Program.startDate.AddSeconds(60))
                throw new ArgumentException("Fake Exception");*/

            await Console.Out.WriteLineAsync($"DONE");
            await Console.Out.WriteLineAsync($"====");
            await Console.Out.WriteLineAsync($"");

            var notificationEvent = new OcrImageProcessedEvent(command.RequestId, "extracted text", processStartDate, DateTime.UtcNow);
            await context.Publish<IOcrImageProcessedEvent>(notificationEvent);
        }
    }
}