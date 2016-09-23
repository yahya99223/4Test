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
        private static readonly object locker = new object();
        private readonly ObservableCollection<ProcessRequest> processRequests;

        public ProcessOcrConsumer(ObservableCollection<ProcessRequest> processRequests)
        {
            this.processRequests = processRequests;
        }

        public async Task Consume(ConsumeContext<IOcrImageProcessedEvent> context)
        {
            var command = context.Message;

            var request = processRequests.FirstOrDefault(r => r.RequestId == command.RequestId);
            if (request == null)
                return;
            lock (locker)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    request.Notifications.Insert(0, $"OCR result:{context.Message.ExtractedText}");
                });
            }
        }
    }
}