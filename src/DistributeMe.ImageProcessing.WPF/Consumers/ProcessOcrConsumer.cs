using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.ViewModels;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessOcrConsumer : IConsumer<IOcrImageProcessedEvent>
    {
        private readonly ObservableCollection<ProcessRequest> processRequests;

        public ProcessOcrConsumer(ObservableCollection<ProcessRequest> processRequests)
        {
            this.processRequests = processRequests;
        }

        public Task Consume(ConsumeContext<IOcrImageProcessedEvent> context)
        {
            var command = context.Message;

            Application.Current.Dispatcher.Invoke(() =>
            {
            var request = processRequests.FirstOrDefault(r => r.RequestId == command.RequestId);
                if (request == null)
                {
                    request = new ProcessRequest
                    {
                        RequestId = command.RequestId
                    };
                    processRequests.Add(request);
                }
                request.Notifications.Insert(0, $"OCR result:{context.Message.ExtractedText}");
            });
            return Task.FromResult<object>(null);
        }
    }
}