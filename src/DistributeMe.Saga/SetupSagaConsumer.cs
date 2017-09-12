using System;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.Saga
{
    internal class SetupSagaConsumer : IConsumer<IProcessCommand>
    {
        private Random random;

        public SetupSagaConsumer()
        {
            random = new Random();
        }

        public async Task Consume(ConsumeContext<IProcessCommand> context)
        {
            var command = context.Message;

            await Console.Out.WriteLineAsync($"Saga is processing new request: {command.RequestId}");

            var processRequestAdded = new ProcessRequestAddedEvent(command.RequestId, command.Data);
            await context.Publish<IProcessRequestAddedEvent>(processRequestAdded);
        }
    }
}