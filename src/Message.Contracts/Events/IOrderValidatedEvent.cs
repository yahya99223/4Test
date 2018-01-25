using System;
using System.Collections.Generic;

namespace Message.Contracts
{
    public interface IOrderValidatedEvent
    {
        Guid OrderId { get; set; }
        int ProcessTime { get; set; }
    }

    public class OrderValidatedEvent : IOrderValidatedEvent
    {
        public Guid OrderId { get; set; }
        public int ProcessTime { get; set; }
    }
}