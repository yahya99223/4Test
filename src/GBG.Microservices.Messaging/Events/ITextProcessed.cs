using System;
using NServiceBus;

namespace GBG.Microservices.Messaging.Events
{
    public interface ITextProcessed : IEvent
    {
        string Sender { get; set; }
        string ProcessedText { get; set; }
        DateTime CreateDate { get; set; }
        DateTime ProcessDate { get; set; }
    }
}