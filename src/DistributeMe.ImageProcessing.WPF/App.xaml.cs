using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DistributeMe.ImageProcessing.WPF.Views;

namespace DistributeMe.ImageProcessing.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IDisposable disposableViewModel = null;

            //Create and show window while storing datacontext
            this.Startup += (sender, args) =>
            {
                MainWindow = new AddProcessOrder();
                disposableViewModel = MainWindow.DataContext as IDisposable;

                MainWindow.Show();
            };

            //Dispose on unhandled exception
            this.DispatcherUnhandledException += (sender, args) =>
            {
                if (disposableViewModel != null)
                    disposableViewModel.Dispose();
            };

            //Dispose on exit
            this.Exit += (sender, args) =>
            {
                if (disposableViewModel != null)
                    disposableViewModel.Dispose();
            };
        }
    }
}
