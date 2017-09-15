using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.Consumers;
using DistributeMe.ImageProcessing.WPF.Helpers;
using DistributeMe.ImageProcessing.WPF.Messages;
using MassTransit;

namespace DistributeMe.ImageProcessing.WPF.ViewModels
{
    public class AddImageProcessOrder : ObservableObject, IDisposable
    {
        private ObservableCollection<ProcessRequest> processRequests;
        private IBusControl bus;

        public AddImageProcessOrder()
        {
            processRequests = new ObservableCollection<ProcessRequest>();
            ProcessSingleCommand = new AsyncRelayCommand(processSingleCommand_Executed);
            ProcessParallelCommand = new AsyncRelayCommand(processParallelCommand_Executed);
            
            bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, MessagingConstants.NotificationQueue, e =>
                {
                    e.Consumer(() => new ProcessRequestAddedConsumer(processRequests));
                    e.Consumer(() => new ProcessOcrConsumer(processRequests));
                    e.Consumer(() => new ProcessFaceConsumer(processRequests));
                    e.Consumer(() => new ProcessFinishedConsumer(processRequests));
                });
            });

            bus.Start();
        }

        public ObservableCollection<ProcessRequest> ProcessRequests
        {
            get { return processRequests; }
            set
            {
                processRequests = value;
                RaisePropertyChanged("ProcessRequests");
            }
        }

        public ICommand ProcessSingleCommand { get; }

        private async Task processSingleCommand_Executed(object obj)
        {
            /*for (int i = 0; i < 16; i++)
            {
                var request = new ProcessRequest
                {
                    RequestId = Guid.NewGuid()
                };
                ProcessRequests.Insert(0, request);

                var processImageCommand = new ProcessImageCommand(request.RequestId, File.ReadAllBytes(@"D:\UserData\Pictures\Untitled.png"));
                await bus.Publish<IProcessImageCommand>(processImageCommand);
                Thread.Sleep(100);
            }*/

            /*var dlg = new OpenFileDialog
            {
                DefaultExt = "JPG Files (*.jpg)|*.jpg",
                Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png"
            };

            var result = dlg.ShowDialog();
            if (result == true)*/

            var request = new ProcessRequest
            {
                RequestId = Guid.NewGuid()
            };
            ProcessRequests.Insert(0, request);

            var processImageCommand = new ProcessCommand(request.RequestId, new byte[] { } /*File.ReadAllBytes(dlg.FileName)*/);
            //await bus.Publish<IProcessCommand>(processImageCommand);

            var sagaEndPointUri = new Uri(MessagingConstants.MqUri + MessagingConstants.SagaQueue);
            var sagaEngineEndPoint = await bus.GetSendEndpoint(sagaEndPointUri);
            await sagaEngineEndPoint.Send<IProcessCommand>(processImageCommand);
        }



        public ICommand ProcessParallelCommand { get; }
        private async Task processParallelCommand_Executed(object arg)
        {
            await Task.Run(() =>
            {
                Parallel.For(0, 100, async (i) =>
                {
                    var request = new ProcessRequest
                    {
                        RequestId = Guid.NewGuid()
                    };
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ProcessRequests.Insert(0, request);
                    });
                    var processImageCommand = new ProcessCommand(request.RequestId, new byte[] { } /*File.ReadAllBytes(dlg.FileName)*/);
                    //await bus.Publish<IProcessCommand>(processImageCommand);

                    var sagaEndPointUri = new Uri(MessagingConstants.MqUri + MessagingConstants.SagaQueue);
                    var sagaEngineEndPoint = await bus.GetSendEndpoint(sagaEndPointUri);
                    await sagaEngineEndPoint.Send<IProcessCommand>(processImageCommand);
                });
            });
        }


        public void Dispose()
        {
            bus.Stop();
        }
    }
}