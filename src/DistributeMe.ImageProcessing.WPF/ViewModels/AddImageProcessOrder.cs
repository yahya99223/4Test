using System;
using System.IO;
using System.Windows.Input;
using DistributeMe.ImageProcessing.WPF.Helpers;
using DistributeMe.ImageProcessing.WPF.Messages;
using Microsoft.Win32;

namespace DistributeMe.ImageProcessing.WPF.ViewModels
{
    public class AddImageProcessOrder : ObservableObject
    {
        private string imagePath;

        public AddImageProcessOrder()
        {
            OpenImageFileCommand = new RelayCommand(openImageFileCommand_Executed);
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                RaisePropertyChanged("ImagePath");
            }
        }

        public ICommand OpenImageFileCommand { get; }

        private void openImageFileCommand_Executed(object obj)
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg"
            };

            var result = dlg.ShowDialog();
            if (result == true)
            {
                ImagePath = dlg.FileName;
                var processImageCommand = new ProcessImageCommand()
                {
                    RequestId = Guid.NewGuid(),
                    Data = File.ReadAllBytes(imagePath),
                };
                using (var bus = new RabbitMqManager())
                {
                    bus.SendProcessImageCommand(processImageCommand);
                }
            }
        }
    }
}