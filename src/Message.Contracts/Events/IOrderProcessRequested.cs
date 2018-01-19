using System;

namespace Message.Contracts.Events
{
    public interface IOrderProcessRequested
    {
        Guid Id { get; set; }
        string OriginalText { get; set; }
    }


    public class OrderProcessRequested : IOrderProcessRequested
    {
        public Guid Id { get; set; }
        public string OriginalText { get; set; }
    }
}