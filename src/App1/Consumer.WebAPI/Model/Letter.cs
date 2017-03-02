using System;

namespace Consumer.WebAPI.Model
{

    public class Letter
    {
        public Letter(Guid id, string from, string to, string title, string body, DateTime? startProcessingDate, DateTime? endProcessingDate)
        {
            Id = id;
            From = from;
            To = to;
            Title = title;
            Body = body;
            StartProcessingDate = startProcessingDate;
            EndProcessingDate = endProcessingDate;
        }


        public Guid Id { get; }
        public string From { get; }
        public string To { get; }
        public string Title { get; }
        public DateTime? StartProcessingDate { get; private set; }
        public DateTime? EndProcessingDate { get; private set; }
        public string Body { get; private set; }


        public void SetBody(string newBody, DateTime startProcessDate, DateTime endProcessDate)
        {
            StartProcessingDate = startProcessDate;
            EndProcessingDate = endProcessDate;
            Body = newBody;
        }
    }
}