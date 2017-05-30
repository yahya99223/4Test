using System;
using NServiceBus;

namespace GBG.Microservices.Messaging.Commands
{
    public class ProcessTextCommand : ICommand
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}