using Autofac;
using Topshelf;

namespace GBG.TextProcessing.ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            // Ioc Helper method for Autofac
            var container = IocConfig.RegisterDependencies();

            return (int)HostFactory.Run(cfg =>
            {
                cfg.Service(s => container.Resolve<MyService>());

                cfg.RunAsLocalSystem();
            });
        }
    }
}