using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using DistributeMe.ImageProcessing.Messaging;
using GreenPipes;
using MassTransit;

namespace DistributeMe.ImageProcessing.Ocr
{
    public static class Program
    {
        public static DateTime startDate;
        private static IBusControl bus;

        #region Nested classes to support running as service

        public const string ServiceName = "ImageOcrRecognitionService";

        public class Service : ServiceBase
        {
            public Service()
            {
                ServiceName = Program.ServiceName;
            }

            protected override void OnStart(string[] args)
            {
                Program.Start(args);
            }

            protected override void OnStop()
            {
                Program.Stop();
            }
        }

        #endregion

        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
                // running as service
                using (var service = new Service())
                    ServiceBase.Run(service);
            else
            {
                // running as console app
                Start(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
        }

        private static void Start(string[] args)
        {
            Console.Title = ServiceName;
            startDate = DateTime.UtcNow;
            bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                /*cfg.UseMessageFilter();
                cfg.UseCircuitBreaker(cb =>
                {
                    cb.ActiveThreshold = 3;
                    cb.TrackingPeriod = TimeSpan.FromSeconds(65);
                    cb.TripThreshold = 70;
                    cb.ResetInterval = TimeSpan.FromMinutes(3);
                });*/

                cfg.ReceiveEndpoint(host, MessagingConstants.ProcessOcrQueue, e =>
                {
                    e.Handler<Fault<ProcessRequestAddedEvent>>(context =>
                    {
                        return Task.FromResult(0);
                    });
                    e.Consumer<ProcessOcrConsumer>();
                });
            });
            bus.Start();

            Console.WriteLine("Listening for Process Image Command to do OCR..");
            Console.ReadKey(true);
        }

        private static void Stop()
        {
            bus.Stop();
        }
    }
}
