using System;
using System.Configuration;
using Autofac;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using Microsoft.ServiceBus;

namespace GBG.TextProcessing.ConsoleApp.Modules
{
    public class AzureServiceBusModule : Module
    {
        private readonly System.Reflection.Assembly[] assembliesToScan;

        public AzureServiceBusModule(params System.Reflection.Assembly[] assembliesToScan)
        {
            this.assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Creates our bus from the factory and registers it as a singleton against two interfaces
            builder.Register(c => Bus.Factory.CreateUsingAzureServiceBus(sbc =>
                {
                    var serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", ConfigurationManager.AppSettings["AzureSbNamespace"], ConfigurationManager.AppSettings["AzureSbPath"]);

                    var host = sbc.Host(serviceUri, h =>
                    {
                        h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(ConfigurationManager.AppSettings["AzureSbKeyName"], ConfigurationManager.AppSettings["AzureSbSharedAccessKey"], TimeSpan.FromDays(1), TokenScope.Namespace);
                    });

                    sbc.ReceiveEndpoint(host, ConfigurationManager.AppSettings["ServiceQueueName"], e =>
                    {
                        // Configure your consumer(s)
                        e.Consumer<ProcessTextHandler>();
                        e.DefaultMessageTimeToLive = TimeSpan.FromMinutes(1);
                        e.EnableDeadLetteringOnMessageExpiration = false;
                    });
                }))
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();
        }
    }
}