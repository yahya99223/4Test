using System;
using System.Collections.Generic;
using System.Configuration;
using Autofac;
using GBG.Microservices.Messaging;
using MassTransit;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Topshelf;
using Topshelf.Logging;

namespace GBG.TextProcessing.ConsoleApp
{
    public class MyService : ServiceControl
    {

        IBusControl busControl;
        private readonly IComponentContext container;
        BusHandle busHandle;
        private string busConnectionString;
        private string topicPath;
        private MessagingFactory messagingFactory;

        public MyService(IBusControl busControl, IComponentContext componentContext)
        {
            this.busControl = busControl;
            this.container = componentContext;
            busConnectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            topicPath = "ProcessTextCommand";
        }

        public bool Start(HostControl hostControl)
        {
            busControl.Start();

            
            var busManager = NamespaceManager.CreateFromConnectionString(busConnectionString);

            if (!busManager.TopicExists(topicPath))
            {
                busManager.CreateTopic(topicPath);
            }
            if (!busManager.SubscriptionExists(topicPath, this.GetType().Namespace))
            {
                var description = new SubscriptionDescription(topicPath, this.GetType().Namespace)
                {
                    AutoDeleteOnIdle = TimeSpan.FromMinutes(5),
                };
                busManager.CreateSubscription(description);
            }

             messagingFactory = MessagingFactory.CreateFromConnectionString(busConnectionString);
            var subscriptionClient = messagingFactory.CreateSubscriptionClient(topicPath, this.GetType().Namespace);

            subscriptionClient.OnMessage(message =>
            {
                var handlers = container.Resolve<IEnumerable<IHandler>>();
                foreach (var handler in handlers)
                {
                    handler.Handle(message);
                }
            });


            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (busHandle != null)
                busHandle.Stop();

            if (messagingFactory != null)
                messagingFactory.Close();

            return true;
        }
    }
}