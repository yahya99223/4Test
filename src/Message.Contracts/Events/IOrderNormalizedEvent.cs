using System;

namespace Message.Contracts
{
    public interface IOrderNormalizedEvent
    {
        Guid OrderId { get; set; }
        int ProcessTime { get; set; }
        string NormalizedText { get; set; }
    }

    public class OrderNormalized : IOrderNormalizedEvent
    {
        public Guid OrderId { get; set; }
        public int ProcessTime { get; set; }
        public string NormalizedText { get; set; }
    }
}