using System;


namespace Shared.Messaging.Messages
{
    public interface ILetter
    {
        Guid Id { get; }
        string From { get; }
        string To { get; }
        string Title { get; }
        string Body { get; set; }
        DateTime StartProcessingDate { get; }
        DateTime? EndProcessingDate { get; }
    }
}