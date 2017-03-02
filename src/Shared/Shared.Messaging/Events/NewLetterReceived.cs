using System;


namespace Shared.Messaging.Events
{
    public class NewLetterReceived
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}