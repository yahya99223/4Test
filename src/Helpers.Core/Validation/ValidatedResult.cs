using System.Collections.Generic;
using System.Linq;

namespace Helpers.Core
{
    //ToDo @Wahid Add documentation for the ValidatedResult class
    /// <summary>
    /// </summary>
    public class ValidatedResult : IValidatedResult
    {
        private readonly HashSet<IViolation> violationsHashSet;

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="violations"></param>
        public ValidatedResult(object data, HashSet<IViolation> violations)
        {
            DataObject = data;
            violationsHashSet = violations ?? new HashSet<IViolation>();
        }


        /// <summary>
        /// </summary>
        public object DataObject { get; }

        /// <summary>
        /// </summary>
        public IViolation[] Violations
        {
            get { return violationsHashSet.ToArray(); }
        }

        /// <summary>
        /// </summary>
        public bool IsValid
        {
            get { return violationsHashSet.All(v => v.Level != ViolationLevel.Error); }
        }


        /*
        public IList<IViolation> GetFlatViolations()
        {
            return getFlatLevel(Violations);
        }

        /// <summary>
        /// Check this ValidatedResult if it's valid or not. If it's not valid an InternalApplicationException with list of Violations will be thrown.
        /// </summary>
        public void Validate()
        {
            if (!IsValid)
                throw new InternalApplicationException(Violations);
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
        }*/
    }



    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidatedResult<T> : ValidatedResult, IValidatedResult<T>
    {
        /// <summary>
        /// </summary>
        public ValidatedResult()
            : base(null, null)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="violations"></param>
        public ValidatedResult(HashSet<IViolation> violations)
            : base(null, violations)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="violations"></param>
        public ValidatedResult(T data, HashSet<IViolation> violations)
            : base(data, violations)
        {
            Data = data;
        }

        public T Data { get; }
    }
}