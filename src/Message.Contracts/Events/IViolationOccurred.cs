using System.Collections.Generic;
using Helpers.Core;

namespace Message.Contracts
{
    public interface IViolationOccurred 
    {
        IList<IViolation> Violations { get; }
    }

    public class ViolationOccurred : IViolationOccurred
    {
        public ViolationOccurred(IList<IViolation> violations)
        {
            Violations = violations??new List<IViolation>();
        }

        public IList<IViolation> Violations { get; }
    }
}