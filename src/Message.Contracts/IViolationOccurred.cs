using System;
using System.Collections.Generic;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IViolationOccurredEvent
    {
        Guid CorrelationId { get; }
        string FriendlyMessage { get; }
        string Service { get; }
        IList<IViolation> Violations { get; }
    }

    public class ViolationOccurredEvent : IViolationOccurredEvent
    {
        public ViolationOccurredEvent(Guid correlationId, string service, string friendlyMessage, IList<IViolation> violations)
        {
            CorrelationId = correlationId;
            FriendlyMessage = friendlyMessage;
            Service = service;
            Violations = violations ?? new List<IViolation>();
        }

        public Guid CorrelationId { get; set; }
        public string FriendlyMessage { get; }
        public string Service { get; }
        public IList<IViolation> Violations { get; }
    }
}