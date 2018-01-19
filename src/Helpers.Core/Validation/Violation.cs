using System;
using System.Collections.Generic;

namespace Helpers.Core
{
    public class Violation : IEquatable<Violation>, IViolation
    {
        public Violation(ViolationLevel level, ViolationType violationType, string key, string message, Exception innerException = null, IList<IViolation> subViolations = null)
        {
            Level = level;
            ViolationType = violationType;
            Key = key;
            Message = message;
            InnerException = innerException;
            SubViolations = subViolations ?? new List<IViolation>();
        }

        public ViolationLevel Level { get; private set; }
        public ViolationType ViolationType { get; private set; }
        public string Key { get; private set; }
        public string Message { get; private set; }
        public Exception InnerException { get; private set; }
        public IList<IViolation> SubViolations { get; private set; }

        public bool Equals(Violation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Level == other.Level && ViolationType == other.ViolationType && string.Equals(Key, other.Key) && string.Equals(Message, other.Message);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Level;
                hashCode = (hashCode*397) ^ (int) ViolationType;
                hashCode = (hashCode*397) ^ (Key != null ? Key.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Message != null ? Message.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Violation left, Violation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Violation left, Violation right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((Violation) obj);
        }
    }
}