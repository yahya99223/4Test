using IDScan.SaaS.SharedBlocks.Helpers.Core;

namespace Core
{
    public class MessageRaisedHandler : IHandles<MessageRaised>
    {
        public void Handle(MessageRaised args)
        {
            args.Message = $"(Handled) {args.Message}";
        }
    }
}