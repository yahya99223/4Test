using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DistributeMe.ImageProcessing.Ocr
{
    /*class Program
    {
        static void Main(string[] args)
        {
        }
    }*/

    public static class Program
    {
        #region Nested classes to support running as service

        public const string ServiceName = "ImageProcessingOcrService";

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
                Console.WriteLine("Listening for Process Image Command  to do OCR..");
                Console.ReadKey();
        }

        private static void Stop()
        {
            // onstop code here
        }
    }


}
