using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace GBG.Microservices.Messaging
{
    public interface IHandler
    {
        void Handle(BrokeredMessage message);
    }
}
