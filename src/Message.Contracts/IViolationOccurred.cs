using System;
using System.Collections.Generic;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IViolationOccurredEvent
    {
        Guid CorrelationId { get; }
        IList<IViolation> Violations { get; }
    }

    public class ViolationOccurredEvent : IViolationOccurredEvent
    {
        public ViolationOccurredEvent(Guid correlationId, IList<IViolation> violations)
        {
            CorrelationId = correlationId;
            Violations = violations ?? new List<IViolation>();
        }

        public Guid CorrelationId { get; set; }
        public IList<IViolation> Violations { get; }
    }
}