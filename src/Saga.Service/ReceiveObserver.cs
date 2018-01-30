using System;
using System.Threading.Tasks;
using MassTransit;

namespace Saga.Service
{
    public class ConsumeObserver : IConsumeObserver
    {
        Task IConsumeObserver.PreConsume<T>(ConsumeContext<T> context)
        {
            Console.WriteLine($"PreConsume: {context.Message}");
            return Task.CompletedTask;
        }

        Task IConsumeObserver.PostConsume<T>(ConsumeContext<T> context)
        {
            // called after the consumer's Consume method is called
            // if an exception was thrown, the ConsumeFault method is called instead
            Console.WriteLine($"PostConsume: {context.Message}");
            return Task.CompletedTask;
        }

        Task IConsumeObserver.ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        {
            Console.WriteLine($"ConsumeFault: {context.Message} - EX: {exception}");
            return Task.CompletedTask;
        }
    }
}