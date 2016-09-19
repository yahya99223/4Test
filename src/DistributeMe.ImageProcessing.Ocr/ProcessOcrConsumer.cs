using System;
using System.Threading;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.Ocr
{
    internal class ProcessOcrConsumer:IConsumer<IProcessImageCommand>
    {
        public async Task Consume(ConsumeContext<IProcessImageCommand> context)
        {
            var command = context.Message;

            await Console.Out.WriteLineAsync($"Processing Request: {command.RequestId}");

            Thread.Sleep(3500);
            //await Task.Run(() => Thread.Sleep(500));


        }
    }
}