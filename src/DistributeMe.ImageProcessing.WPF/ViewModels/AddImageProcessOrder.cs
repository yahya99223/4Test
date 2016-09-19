using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using DistributeMe.ImageProcessing.Messaging;
using DistributeMe.ImageProcessing.WPF.Helpers;
using DistributeMe.ImageProcessing.WPF.Messages;
using Microsoft.Win32;

namespace DistributeMe.ImageProcessing.WPF.ViewModels
{
    public class AddImageProcessOrder : ObservableObject,IDisposable
    {
        private ObservableCollection<ProcessRequest> processRequests;

        public AddImageProcessOrder()
        {
            OpenImageFileCommand = new AsyncRelayCommand(openImageFileCommand_Executed);
            processRequests = new ObservableCollection<ProcessRequest>();
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

        public ICommand OpenImageFileCommand { get; }

        private async Task openImageFileCommand_Executed(object obj)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg"
            };

            var result = dlg.ShowDialog();
            if (result == true)
            {
                var request = new ProcessRequest
                {
                    RequestId = Guid.NewGuid()
                };
                ProcessRequests.Insert(0, request);
                var processImageCommand = new ProcessImageCommand(request.RequestId, File.ReadAllBytes(dlg.FileName));

                var bus = BusConfigurator.ConfigureBus();

                var faceEngineEndPointUri = new Uri(MessagingConstants.MqUri+MessagingConstants.ProcessFaceQueue);
                var faceEngineEndPoint = await bus.GetSendEndpoint(faceEngineEndPointUri);
                await faceEngineEndPoint.Send<IProcessImageCommand>(processImageCommand);

                var ocrEngineEndPointUri = new Uri(MessagingConstants.MqUri+MessagingConstants.ProcessOcrQueue);
                var ocrEngineEndPoint = await bus.GetSendEndpoint(ocrEngineEndPointUri);
                await ocrEngineEndPoint.Send<IProcessImageCommand>(processImageCommand);
            }
        }

        public void Dispose()
        {

        }
    }
}