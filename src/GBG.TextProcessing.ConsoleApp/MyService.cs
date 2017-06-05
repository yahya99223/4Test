using MassTransit;
using Topshelf;
using Topshelf.Logging;

namespace GBG.TextProcessing.ConsoleApp
{
    public class MyService : ServiceControl
    {

        IBusControl busControl;
        BusHandle busHandle;

        public MyService(IBusControl busControl)
        {
            this.busControl = busControl;
        }

        public bool Start(HostControl hostControl)
        {
            busControl.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (busHandle != null)
                busHandle.Stop();

            return true;
        }
    }
}