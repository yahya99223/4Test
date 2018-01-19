using System;

namespace Message.Contracts.Events
{
    public interface IOrderCapitalized
    {
        Guid OrderId { get; set; }
        string NormalizedText { get; set; }
    }

    public class OrderCapitalized : IOrderCapitalized
    {
        public Guid OrderId { get; set; }
        public string NormalizedText { get; set; }
    }
}