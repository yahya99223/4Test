using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Core;
using IDScan.SaaS.SharedBlocks.Helpers.Core;

namespace IoC.Installer
{
    public class HiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SomeService>().As<ISomeService>().InstancePerRequest();
            builder.RegisterType<AnotherService>().As<IAnotherService>().InstancePerRequest();

            builder.RegisterType<MessageRaisedHandler>().As<IHandles<MessageRaised>>();
            builder.RegisterType<MessageProcessedHandler>().As<IHandles<MessageProcessed>>();
        }
    }
}
