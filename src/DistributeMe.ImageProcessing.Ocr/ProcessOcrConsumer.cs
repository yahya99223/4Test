using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using IDScan.SaaS.SharedBlocks.Helpers.Core;
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
            
            if (command.RequestId.ToString().Contains("8") && command.RequestId.ToString().Contains("9"))
                throw new InternalApplicationException<IProcessRequestAddedEvent>(x => x.RequestId, ViolationType.NotAllowed);

            Console.WriteLine($"FINISHED {command.RequestId}");

            var notificationEvent = new OcrProcessedEvent(command.RequestId, "extracted text", processStartDate, DateTime.UtcNow);
            await context.Publish<IOcrProcessedEvent>(notificationEvent);
        }
    }
}