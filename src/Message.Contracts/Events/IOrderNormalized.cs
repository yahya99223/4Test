using System;

namespace Message.Contracts.Events
{
    public interface IOrderNormalized
    {
        Guid OrderId { get; set; }
        string NormalizedText { get; set; }
    }

    public class OrderNormalized : IOrderNormalized
    {
        public Guid OrderId { get; set; }
        public string NormalizedText { get; set; }
    }
}