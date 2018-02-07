using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers.Core
{
    public static class ViolationExtensions
    {
        public static string FriendlyMessage(this IEnumerable<IViolation> violations)
        {
            return string.Join("- " + Environment.NewLine, violations.Select(v => v.Message));
        }
    }
}