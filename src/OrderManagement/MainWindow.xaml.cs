using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MassTransit;
using OrderManagement.DbModel;

namespace OrderManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBusControl BusControl;

        public MainWindow()
        {
            InitializeComponent();
            BusControl = BusHelper.GetBusControl();

            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            
        }

        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            BusControl?.Stop();
        }

        private void btnDeleteAllFinished_Click(object sender, RoutedEventArgs e)
        {
            var source = dgOrders.Items;
            
        }

     
    }
}
