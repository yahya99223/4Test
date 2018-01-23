using System.Collections.Generic;
using System.Linq;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IValidatedMessage
    {
        bool IsValid { get; }
        IList<IViolation> Violations { get; }
    }

    public class ValidatedMessage : IValidatedMessage
    {
        public ValidatedMessage(IList<IViolation> violations)
        {
            Violations = violations ?? new List<IViolation>();
        }

        public bool IsValid => Violations.All(v => v.Level != ViolationLevel.Error);
        public IList<IViolation> Violations { get; }
    }
}