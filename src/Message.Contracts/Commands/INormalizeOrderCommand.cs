using System;

namespace Message.Contracts
{
    public interface INormalizeOrderCommand
    {
        Guid OrderId { get; set; }
        string OriginalText { get; set; }
    }

    public class NormalizeOrderCommand : INormalizeOrderCommand
    {
        public Guid OrderId { get; set; }
        public string OriginalText { get; set; }
    }
}