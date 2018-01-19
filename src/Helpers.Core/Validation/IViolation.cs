using System.Collections.Generic;

namespace Helpers.Core
{
    public interface IViolation
    {
        string Key { get; }
        ViolationLevel Level { get; }
        string Message { get; }
        IList<IViolation> SubViolations { get; }
        ViolationType ViolationType { get; }
    }
}