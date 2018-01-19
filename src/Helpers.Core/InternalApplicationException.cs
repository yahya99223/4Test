using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Helpers.Core
{
    public class InternalApplicationException<T> : InternalApplicationException
    {

        /// <summary>
        ///     Register new violation to the current ViolationHandler instance
        /// </summary>
        public InternalApplicationException(Expression<Func<T, object>> keyPath, ViolationType violationType = ViolationType.General, string message = null, ViolationLevel level = ViolationLevel.Error)
        {
            var keyPathString = ReflectionHelpers.GetPropertyPath(keyPath);
            message = message ?? ViolationHandler.GetAutomatedErrorMessage(typeof(T), keyPathString, violationType);
            var key = ViolationHandler.GenerateKey(keyPath, violationType);
            Violations.Add(new Violation(level, violationType, key, message));
        }
    }

    public class InternalApplicationException : Exception
    {
        public IList<IViolation> Violations { get; private set; }


        public Dictionary<string, List<string>> GetErrorsDictionary()
        {
            var flatViolations = GetFlatViolations();
            return flatViolations
                .GroupBy(g => g.Key)
                .ToDictionary(x => x.Key, x => x.Select(e => e.Message).ToList());
        }


        public IList<IViolation> GetFlatViolations()
        {
            return getFlatLevel(Violations);
        }

        private IList<IViolation> getFlatLevel(IList<IViolation> violations, IList<IViolation> flatViolations = null)
        {
            if (flatViolations == null)
                flatViolations = new List<IViolation>();
            foreach (var violation in violations)
            {
                if (violation.SubViolations.Any())
                    flatViolations.AddRange(getFlatLevel(violation.SubViolations, flatViolations));
                else
                    flatViolations.Add(violation);
            }
            return flatViolations;
        }

        protected InternalApplicationException()
        {
            Violations = new List<IViolation>();
        }


        /// <summary>
        ///     Register list of violations to the current ViolationHandler instance
        /// </summary>
        /// <param name="violationsSubList"></param>
        public InternalApplicationException(IEnumerable<IViolation> violationsSubList)
            : this()
        {
            Violations.AddRange(violationsSubList);
        }


        /// <summary>
        ///     Register new violation to the current ViolationHandler instance
        /// </summary>
        public InternalApplicationException(string key, ViolationType violationType = ViolationType.General, string message = null, ViolationLevel level = ViolationLevel.Error)
            : this()
        {
            message = message ?? ViolationHandler.GetAutomatedErrorMessage(null, key, violationType);
            Violations.Add(new Violation(level, violationType, key, message));
        }

        public override string Message
        {
            get { return string.Join(Environment.NewLine + "    -> ", GetFlatViolations().Select(v => v.Message)); }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine();
            builder.AppendLine(Message);
            return builder.ToString();
        }
    }
}