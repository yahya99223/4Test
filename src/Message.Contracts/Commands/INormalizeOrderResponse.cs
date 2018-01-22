using System;

namespace Message.Contracts
{
    public interface INormalizeOrderResponse
    {
        Guid OrderId { get; set; }
        DateTime StartProcessTime { get; set; }
        DateTime EndProcessTime { get; set; }
        string NormalizedText { get; set; }
    }

    public class NormalizeOrderResponse : INormalizeOrderResponse
    {
        public Guid OrderId { get; set; }
        public DateTime StartProcessTime { get; set; }
        public DateTime EndProcessTime { get; set; }
        public string NormalizedText { get; set; }
    }
}