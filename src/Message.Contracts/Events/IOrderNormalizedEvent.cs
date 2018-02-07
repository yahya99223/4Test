using System;
using System.Collections.Generic;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IOrderNormalizedEvent : IValidatedMessage
    {
        Guid OrderId { get; set; }
        int ProcessTime { get; set; }
        string NormalizedText { get; set; }
    }

    public class OrderNormalized : ValidatedMessage, IOrderNormalizedEvent
    {
        public OrderNormalized(Guid orderId, IList<IViolation> violations = null) : base(orderId, violations)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
        public int ProcessTime { get; set; }
        public string NormalizedText { get; set; }
    }
}