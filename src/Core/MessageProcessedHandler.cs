using IDScan.SaaS.SharedBlocks.Helpers.Core;

namespace Core
{
    public class MessageProcessedHandler : IHandles<MessageProcessed>
    {
        private readonly IAnotherService anotherService;

        public MessageProcessedHandler(IAnotherService anotherService)
        {
            this.anotherService = anotherService;
        }

        public void Handle(MessageProcessed args)
        {
            args.Message = anotherService.Decorate(args.Message);
        }
    }
}