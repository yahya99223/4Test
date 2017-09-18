using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.ViewModels;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.Consumers
{
    public class ProcessFinishedConsumer : IConsumer<IProcessRequestFinishedEvent>
    {
        private readonly ObservableCollection<ProcessRequest> processRequests;

        public ProcessFinishedConsumer(ObservableCollection<ProcessRequest> processRequests)
        {
            this.processRequests = processRequests;
        }

        public async Task Consume(ConsumeContext<IProcessRequestFinishedEvent> context)
        {
            var command = context.Message;
            if (App.RemovedRequests.Contains(command.RequestId))
                return;

            await Task.Delay(200);

            ProcessRequest request;
            lock (App.Locker)
            {
                request = processRequests.FirstOrDefault(r => r.RequestId == command.RequestId);
                if (request == null)
                {
                    request = new ProcessRequest
                    {
                        RequestId = command.RequestId
                    };
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        processRequests.Add(request);
                    });
                }
            }

            Application.Current.Dispatcher.Invoke(() => { request.Notifications.Insert(0, $"Request Processing Finished"); });
            if (request.Notifications.Count >= 4)
            {
                await Task.Run(async () =>
                {
                    await Task.Delay(1500);
                    lock (App.Locker)
                    {
                        App.RemovedRequests.Add(request.RequestId);
                        Application.Current.Dispatcher.Invoke(() => { processRequests.Remove(request); });
                    }
                });
            }
        }
    }
}