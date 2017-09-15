using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.ViewModels;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessRequestAddedConsumer : IConsumer<IProcessRequestAddedEvent>
    {
        private readonly ObservableCollection<ProcessRequest> processRequests;

        public ProcessRequestAddedConsumer(ObservableCollection<ProcessRequest> processRequests)
        {
            this.processRequests = processRequests;
        }

        public Task Consume(ConsumeContext<IProcessRequestAddedEvent> context)
        {
            var command = context.Message;

            var request = processRequests.FirstOrDefault(r => r.RequestId == command.RequestId);
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (request == null)
                {
                    request = new ProcessRequest
                    {
                        RequestId = command.RequestId
                    };
                    processRequests.Add(request);
                }
                request.Notifications.Insert(0, $"Request Added to the Queue");
            });
            return Task.FromResult<object>(null);
        }
    }
}