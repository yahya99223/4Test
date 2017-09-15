﻿using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.FaceRecognition.Messages;
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

            Console.WriteLine($"FINISHED {command.RequestId}");

            var notificationEvent = new FaceRecognitionImageProcessedEvent(command.RequestId, 2, processStartDate, DateTime.UtcNow);
            await context.Publish<IFaceRecognitionImageProcessedEvent>(notificationEvent);
        }
    }
}