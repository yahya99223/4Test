using System.Reflection;
using Autofac;
using GBG.Microservices.Messaging;
using GBG.TextProcessing.ConsoleApp.Modules;

namespace GBG.TextProcessing.ConsoleApp
{
    public class IocConfig
    {
        public static IContainer RegisterDependencies()
        {
            //IContainer container = new ContainerBuilder().Build();
            var builder = new ContainerBuilder();

            //builder.RegisterInstance(container).SingleInstance().As<IContainer>();

            builder.RegisterType<MyService>();
            builder.RegisterType<ProcessTextNativeHandler>().As<IHandler>();

            builder.RegisterModule(new AzureServiceBusModule(Assembly.GetExecutingAssembly()));

            var container = builder.Build();
            
            return container;
        }
    }
}