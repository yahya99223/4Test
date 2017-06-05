using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using Autofac;
using GBG.TextProcessing.ConsoleApp.Modules;
using MassTransit;

namespace GBG.TextProcessing.ConsoleApp
{
    public static class Program
    {
        private static IBusControl bus;

        #region Nested classes to support running as service

        public const string ServiceName = "TextProcessing Service";

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

            var builder = new ContainerBuilder();

            builder.RegisterType<Service>();

            builder.RegisterModule(new AzureServiceBusModule(Assembly.GetExecutingAssembly()));

            var builderInstance =  builder.Build();

            Console.WriteLine("Listening for Process Image Command to do FaceRecognition..");
            Console.ReadKey(true);
        }

        private static void Stop()
        {
            bus.Stop();
        }
    }
}
