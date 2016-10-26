using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.FaceRecognition.Messages;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.FaceRecognition
{
    internal class ProcessFaceConsumer : IConsumer<IProcessImageCommand>
    {
        private Random random;

        public ProcessFaceConsumer()
        {
            random = new Random();
        }

        public async Task Consume(ConsumeContext<IProcessImageCommand> context)
        {
            var command = context.Message;

            await Console.Out.WriteLineAsync($"Processing Request: {command.RequestId}");

            var processStartDate = DateTime.UtcNow;
            Thread.Sleep(random.Next(500, 2000));

            await Console.Out.WriteLineAsync($"DONE");
            await Console.Out.WriteLineAsync($"====");
            await Console.Out.WriteLineAsync($"");

            var notificationEvent = new FaceRecognitionImageProcessedEvent(command.RequestId, 2, processStartDate, DateTime.UtcNow);
            await context.Publish<IFaceRecognitionImageProcessedEvent>(notificationEvent);
        }
    }
}