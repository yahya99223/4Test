using System.Windows.Input;
using DistributeMe.ImageProcessing.WPF.Helpers;

namespace DistributeMe.ImageProcessing.WPF.ViewModels
{
    public class AddImageProcessOrder : ObservableObject
    {
        private string imagePath;

        public AddImageProcessOrder()
        {
            OpenImageFileCommand = new RelayCommand(openImageFileCommand_Executed);
        }

        public ICommand OpenImageFileCommand { get; }

        private void openImageFileCommand_Executed(object obj)
        {

        }
    }
}