using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.Saga
{
    public class ProcessFaultConsumer :
        IConsumer<Fault<IProcessCommand>>,
        IConsumer<Fault<ProcessCommand>>,
        IConsumer<Fault<IProcessRequestAddedEvent>>,
        IConsumer<Fault<ProcessRequestAddedEvent>>,
        IConsumer<Fault<IOcrProcessedEvent>>,
        IConsumer<Fault<OcrProcessedEvent>>,
        IConsumer<Fault<IFaceProcessedEvent>>,
        IConsumer<Fault<FaceProcessedEvent>>,
        IConsumer<Fault<IProcessRequestFinishedEvent>>,
        IConsumer<Fault<ProcessRequestFinishedEvent>>
    {
        public Task Consume(ConsumeContext<Fault<ProcessRequestFinishedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IOcrProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IFaceProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<ProcessRequestAddedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IProcessCommand>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<ProcessCommand>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IProcessRequestAddedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<OcrProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<FaceProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IProcessRequestFinishedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }
    }
}