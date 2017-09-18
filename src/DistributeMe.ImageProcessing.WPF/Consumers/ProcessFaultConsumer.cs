using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessFaultConsumer :
        IConsumer<Fault<IProcessRequestAddedEvent>>,
        IConsumer<Fault<IOcrImageProcessedEvent>>,
        IConsumer<Fault<IFaceRecognitionImageProcessedEvent>>,
        IConsumer<Fault<IProcessRequestFinishedEvent>>
    {
        public Task Consume(ConsumeContext<Fault<IProcessRequestFinishedEvent>> context)
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

        public Task Consume(ConsumeContext<Fault<IProcessRequestAddedEvent>> context)
        {
            return Task.FromResult<object>(null);
        }
    }
}