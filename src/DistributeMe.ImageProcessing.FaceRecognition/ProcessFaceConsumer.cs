using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.FaceRecognition
{
    internal class ProcessFaceConsumer : IConsumer<IProcessRequestAddedEvent>
    {
        private Random random;

        public ProcessFaceConsumer()
        {
            random = new Random();
        }

        public async Task Consume(ConsumeContext<IProcessRequestAddedEvent> context)
        {
            var command = context.Message;

            Console.WriteLine($"Processing Request: {command.RequestId}");

            var processStartDate = DateTime.UtcNow;
            await Task.Delay(random.Next(500, 5000));

            /*if (random.Next(1, 9) % 2 == 0)
                throw new Exception("There is no face");*/

            Console.WriteLine($"FINISHED {command.RequestId}");

            var notificationEvent = new FaceProcessedEvent(command.RequestId, 2, processStartDate, DateTime.UtcNow);
            await context.Publish<IFaceProcessedEvent>(notificationEvent);
        }
    }
}