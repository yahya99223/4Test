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
}