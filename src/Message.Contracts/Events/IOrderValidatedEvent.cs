using System;
using System.Collections.Generic;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IOrderValidatedEvent : IValidatedMessage
    {
        Guid OrderId { get; }
        DateTime StartProcessTime { get; }
        DateTime EndProcessTime { get; }
    }

    public class OrderValidatedEvent : ValidatedMessage, IOrderValidatedEvent
    {
        public OrderValidatedEvent(Guid orderId, IList<IViolation> violations = null) : base(orderId, violations)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }

        public DateTime StartProcessTime { get; set; }

        public DateTime EndProcessTime { get; set; }
    }
}