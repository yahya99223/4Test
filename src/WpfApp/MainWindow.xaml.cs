using System;
using System.Windows;
using DomainModel;

namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CaptureSession captureSession;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var maxPageCount = int.Parse(txtMaximumPageCount.Text);
                captureSession = DocumentCaptureSession.Create(maxPageCount);
                Console.WriteLine($"-> CaptureSession {captureSession.Id} is {captureSession.State}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("====================EXCEPTION====================");
                Console.WriteLine(ex);
                Console.WriteLine("=================================================");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                captureSession?.AddProcessRequest(txtProcessRequestData.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("====================EXCEPTION====================");
                Console.WriteLine(ex);
                Console.WriteLine("=================================================");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                captureSession?.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine("====================EXCEPTION====================");
                Console.WriteLine(ex);
                Console.WriteLine("=================================================");
            }
        }
    }
}