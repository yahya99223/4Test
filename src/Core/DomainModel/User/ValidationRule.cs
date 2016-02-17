using System.Collections.Generic;

namespace Core.DomainModel.User
{
    public abstract class ValidationRule
    {
        public abstract string Name { get; }

        public abstract IList<string> Validate(User user);
    }
}