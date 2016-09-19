using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Notification
{
    public static class Program
    {
        #region Nested classes to support running as service

        public const string ServiceName = "NotificationService";

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
            Console.Title = "Notification service";
            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.ListenForFaceProcessImageEvent();
                rabbitMqManager.ListenForOcrProcessImageEvent();
                Console.WriteLine("Listening for Notifications..");
                Console.ReadKey();
            }
        }

        private static void Stop()
        {
            // onstop code here
        }
    }

}
