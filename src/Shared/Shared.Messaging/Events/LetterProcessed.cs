using System;


namespace Shared.Messaging.Events
{
    public class LetterProcessed
    {
        public Guid Id { get; set; }
        public DateTime ProcessStartDate { get; set; }
        public string UpdatedBody { get; set; }
        public DateTime ProcessEndDate { get; set; }
    }
}