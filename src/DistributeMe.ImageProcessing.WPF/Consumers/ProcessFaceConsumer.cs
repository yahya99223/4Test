using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.ViewModels;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessFaceConsumer : IConsumer<IFaceRecognitionImageProcessedEvent>
    {
        private static readonly object locker = new object();
        private readonly ObservableCollection<ProcessRequest> processRequests;

        public ProcessFaceConsumer(ObservableCollection<ProcessRequest> processRequests)
        {
            this.processRequests = processRequests;
        }

        public async Task Consume(ConsumeContext<IFaceRecognitionImageProcessedEvent> context)
        {
            var command = context.Message;

            var request = processRequests.FirstOrDefault(r => r.RequestId == command.RequestId);
            if (request == null)
                return;
            lock (locker)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    request.Notifications.Insert(0, $"Face(s) count:{context.Message.FacesCount}");
                });
            }
        }
    }
}