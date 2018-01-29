using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;

namespace Saga.Service
{
    public static class ViolationHandlerFilterExtensions
    {
        public static void UseViolationHandler<T>(this IPipeConfigurator<T> cfg) where T : class, PipeContext
        {
            cfg.AddPipeSpecification(new ViolationHandlerSpecification<T>());
        }
    }

    public class ViolationHandlerSpecification<T> : IPipeSpecification<T>
        where T : class, PipeContext
    {
        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new ViolationHandlerFilter<T>());
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return new List<ValidationResult>();
        }
    }

    public class ViolationHandlerFilter<T> : IFilter<T> where T : class, PipeContext
    {
        long exceptionCount;
        long successCount;
        long attemptCount;

        public async Task Send(T context, IPipe<T> next)
        {

            await next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
           /* var scope = context.CreateFilterScope("violationHandler");
            scope.Add("attempted", attemptCount);
            scope.Add("succeeded", successCount);
            scope.Add("faulted", exceptionCount);*/
        }
    }
}