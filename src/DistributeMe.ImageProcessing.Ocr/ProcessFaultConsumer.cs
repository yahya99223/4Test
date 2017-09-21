using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using MassTransit;

namespace DistributeMe.ImageProcessing.Ocr
{
    public class ProcessFaultConsumer :
        /*IConsumer<Fault<IProcessCommand>>,
        IConsumer<Fault<ProcessCommand>>,
        IConsumer<Fault<IProcessRequestAddedEvent>>,*/
        IConsumer<Fault<ProcessRequestAddedEvent>>/*,
        IConsumer<Fault<IOcrImageProcessedEvent>>,
        IConsumer<Fault<OcrImageProcessedEvent>>,
        IConsumer<Fault<IFaceRecognitionImageProcessedEvent>>,
        IConsumer<Fault<FaceRecognitionImageProcessedEvent>>,
        IConsumer<Fault<IProcessRequestFinishedEvent>>,
        IConsumer<Fault<ProcessRequestFinishedEvent>>*/
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

        public async Task Consume(ConsumeContext<Fault<ProcessRequestAddedEvent>> context)
        {
            await Task.Run(() =>
            {
                var x = context.Message.Message;
                var y = context.Message.Exceptions;
            });
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