using IDScan.SaaS.SharedBlocks.Helpers.Core;

namespace Core
{
    public class MessageRaised : IDomainEvent
    {
        public string Message { get; set; }

        public MessageRaised(string message)
        {
            Message = message;
        }
    }

    public class MessageProcessed : IDomainEvent
    {
        public string Message { get; set; }

        public MessageProcessed(string message)
        {
            Message = message;
        }
    }
}