using System;

namespace Message.Contracts
{
    public interface IOrderCapitalizedEvent
    {
        Guid OrderId { get; set; }
        int ProcessTime { get; set; }
        string CapitalizedText { get; set; }

    }

    public class OrderCapitalized : IOrderCapitalizedEvent
    {
        public Guid OrderId { get; set; }
        public int ProcessTime { get; set; }
        public string CapitalizedText { get; set; }
    }
}