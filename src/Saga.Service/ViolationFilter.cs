using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using Message.Contracts;

namespace Saga.Service
{
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
            var scope = context.CreateFilterScope("violationHandler");
            scope.Add("attempted", attemptCount);
            scope.Add("succeeded", successCount);
            scope.Add("faulted", exceptionCount);
        }
    }

    public class ViolationHandlerSpecification<T> :
        IPipeSpecification<T>
        where T : class, PipeContext
    {
        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new ViolationHandlerFilter<T>());
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }
    }
}