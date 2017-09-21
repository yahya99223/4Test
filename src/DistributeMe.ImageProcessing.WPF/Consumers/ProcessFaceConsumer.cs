using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.ViewModels;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessFaceConsumer : IConsumer<IFaceProcessedEvent>
    {
        private readonly ObservableCollection<ProcessRequest> processRequests;

        public ProcessFaceConsumer(ObservableCollection<ProcessRequest> processRequests)
        {
            this.processRequests = processRequests;
        }

        public Task Consume(ConsumeContext<IFaceProcessedEvent> context)
        {
            var command = context.Message;
            if (App.RemovedRequests.Contains(command.RequestId))
                return Task.FromResult<object>(null);

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

                request.Notifications.Insert(0, $"Face(s) count:{context.Message.FacesCount}");
            });
            return Task.FromResult<object>(null);
        }
    }
}