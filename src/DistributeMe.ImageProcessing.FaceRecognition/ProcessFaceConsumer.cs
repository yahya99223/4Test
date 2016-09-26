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
        public async Task Consume(ConsumeContext<IProcessImageCommand> context)
        {
            var command = context.Message;

            await Console.Out.WriteLineAsync($"Processing Request: {command.RequestId}");

            var processStartDate = DateTime.UtcNow;
            Thread.Sleep(2500);

            await Console.Out.WriteLineAsync($"DONE");

            var notificationEvent = new FaceRecognitionImageProcessedEvent(command.RequestId, 2, processStartDate, DateTime.UtcNow);
            await context.Publish<IFaceRecognitionImageProcessedEvent>(notificationEvent);

        }
    }
}