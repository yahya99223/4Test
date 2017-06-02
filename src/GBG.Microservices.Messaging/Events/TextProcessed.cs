using System;

namespace GBG.Microservices.Messaging.Events
{
    public class TextProcessed
    {
        public string Sender { get; set; }
        public string ProcessedText { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ProcessDate { get; set; }
    }
}