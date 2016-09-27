using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Configurators;
using MassTransit.PipeBuilders;
using MassTransit.PipeConfigurators;
using MassTransit.Pipeline;

namespace DistributeMe.ImageProcessing.Messaging
{
    public static class MessageMiddlewareExtensions
    {
        public static void UseMessageFilter<T>(this IPipeConfigurator<T> configurator) where T : class, PipeContext
        {
            configurator.AddPipeSpecification(new MessageFilterSpecification<T>());
        }
    }


    public class MessageFilterSpecification<T> : IPipeSpecification<T> where T : class, PipeContext
    {
        public IEnumerable<ValidationResult> Validate()
        {
            return new List<ValidationResult>();
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new MessageFilter<T>());
        }
    }


    public class MessageFilter<T> : IFilter<T> where T : class, PipeContext
    {
        public void Probe(ProbeContext context)
        {
            // update the context if needed
        }

        public async Task Send(T context, IPipe<T> next)
        {
            Console.WriteLine($"Audit: Message comming to OCR Engine.");
            await next.Send(context);
        }
    }
}