﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IValidatedMessage
    {
        Guid CorrelationId { get; }
        bool IsValid { get; }
        IList<IViolation> Violations { get; }
    }

    public class ValidatedMessage : IValidatedMessage
    {
        public ValidatedMessage(Guid correlationId, IList<IViolation> violations)
        {
            CorrelationId = correlationId;
            Violations = violations ?? new List<IViolation>();
        }

        public Guid CorrelationId { get; }
        public bool IsValid => Violations.All(v => v.Level != ViolationLevel.Error);
        public IList<IViolation> Violations { get; }
    }
}