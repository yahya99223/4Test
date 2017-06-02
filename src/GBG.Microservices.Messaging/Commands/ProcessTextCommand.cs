using System;

namespace GBG.Microservices.Messaging.Commands
{
    public class ProcessTextCommand 
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}