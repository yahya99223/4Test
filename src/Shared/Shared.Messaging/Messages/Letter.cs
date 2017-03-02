using System;


namespace Shared.Messaging.Messages
{
    public class Letter : ILetter
    {
        private string body;


        public Letter(Guid id, string from, string to, string title, string body, DateTime startProcessingDate, DateTime? endProcessingDate)
        {
            Id = id;
            From = from;
            To = to;
            Title = title;
            this.body = body;
            StartProcessingDate = startProcessingDate;
            EndProcessingDate = endProcessingDate;
        }


        public Guid Id { get; }
        public string From { get; }
        public string To { get; }
        public string Title { get; }


        public string Body
        {
            get { return body; }
            set
            {
                EndProcessingDate = DateTime.UtcNow;
                body = value;
            }
        }


        public DateTime StartProcessingDate { get; }
        public DateTime? EndProcessingDate { get; private set; }
    }
}