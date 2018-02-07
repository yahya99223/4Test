using System;
using System.Collections.Generic;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IOrderCapitalizedEvent : IValidatedMessage
    {
        Guid OrderId { get; }
        int ProcessTime { get; }
        string CapitalizedText { get; }

    }

    public class OrderCapitalized : ValidatedMessage, IOrderCapitalizedEvent
    {
        public OrderCapitalized(Guid orderId, IList<IViolation> violations = null) : base(orderId, violations)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
        public int ProcessTime { get; set; }
        public string CapitalizedText { get; set; }
    }
}