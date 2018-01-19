using System;

namespace Message.Contracts.Events
{
    public interface IOrderProcessRequested
    {
        Guid OrderId { get; set; }
        string OriginalText { get; set; }
    }


    public class OrderProcessRequested : IOrderProcessRequested
    {
        public Guid OrderId { get; set; }
        public string OriginalText { get; set; }
    }
}