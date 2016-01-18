using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int messageNumber;
        private static object locker = new object();


        public MainWindow()
        {
            messageNumber = 0;
            //service = new FakeService();
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    messageNumber += 1;
                }
                var service= new FakeService();
                var message = service.Process(messageNumber);
                MessageBox.Show(message);
            });
        }
    }



    public class FakeService
    {
        private static ConcurrentDictionary<int, AutoResetEvent> autoResetEvents = new ConcurrentDictionary<int, AutoResetEvent>();
        private string val;
        private Random random;


        public FakeService()
        {
            val = "NON";
            random = new Random();
        }


        public string Process(int messageNumber)
        {
            var autoReset = new AutoResetEvent(false);
            autoResetEvents.TryAdd(messageNumber, autoReset);
            doWork(messageNumber);
            autoReset.WaitOne(3000);
            return val;
        }


        private void doWork(int messageNumber)
        {
            Task.Factory.StartNew(() =>
            {
                var sleepTime = random.Next(1000, 8000);
                Thread.Sleep(sleepTime);
                val = sleepTime.ToString() + " For message number " + messageNumber;

                AutoResetEvent autoReset;
                if (autoResetEvents.TryRemove(messageNumber, out autoReset))
                {
                    autoReset.Set();
                }
                else
                {
                    throw new Exception("autoResetEvent not found");
                }
            });
        }
    }
}