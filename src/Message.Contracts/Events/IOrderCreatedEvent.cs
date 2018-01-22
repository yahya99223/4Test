using System;
using System.Collections.Generic;

namespace Message.Contracts
{
    public interface IOrderCreatedEvent
    {
        DateTime CreateDate { get; set; }
        Guid OrderId { get; set; }
        string OriginalText { get; set; }
        ICollection<string> Services { get; set; }
    }

    public class OrderCreated : IOrderCreatedEvent
    {
        public OrderCreated()
        {
            Services = new List<string>();
        }

        public Guid OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public string OriginalText { get; set; }
        public ICollection<string> Services { get; set; }
    }
}