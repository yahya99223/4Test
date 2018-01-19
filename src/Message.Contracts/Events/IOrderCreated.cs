using System;
using System.Collections.Generic;

namespace Message.Contracts.Events
{
    public interface IOrderCreated
    {
        DateTime CreateDate { get; set; }
        Guid Id { get; set; }
        string OriginalText { get; set; }
        ICollection<string> Services { get; set; }
    }

    public class OrderCreated : IOrderCreated
    {
        public OrderCreated()
        {
            Services = new List<string>();
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string OriginalText { get; set; }
        public ICollection<string> Services { get; set; }
    }
}