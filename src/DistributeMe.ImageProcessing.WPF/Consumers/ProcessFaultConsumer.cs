using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessFaultConsumer :
        IConsumer<Fault<IProcessCommand>>,
        IConsumer<Fault<ProcessCommand>>,
        IConsumer<Fault<IProcessRequestAddedEvent>>,
        IConsumer<Fault<ProcessRequestAddedEvent>>,
        IConsumer<Fault<IOcrImageProcessedEvent>>,
        IConsumer<Fault<OcrImageProcessedEvent>>,
        IConsumer<Fault<IFaceRecognitionImageProcessedEvent>>,
        IConsumer<Fault<FaceRecognitionImageProcessedEvent>>,
        IConsumer<Fault<IProcessRequestFinishedEvent>>,
        IConsumer<Fault<ProcessRequestFinishedEvent>>
    {
        public Task Consume(ConsumeContext<Fault<ProcessRequestFinishedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IOcrImageProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IFaceRecognitionImageProcessedEvent>> context)
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

        public Task Consume(ConsumeContext<Fault<OcrImageProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<FaceRecognitionImageProcessedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }

        public Task Consume(ConsumeContext<Fault<IProcessRequestFinishedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }
    }
}