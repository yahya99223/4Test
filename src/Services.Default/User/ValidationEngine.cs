using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.DomainModel.User;

namespace Services.Default.User
{
    public class ValidationEngine
    {
        private readonly IServiceResolver serviceResolver;


        public ValidationEngine(IServiceResolver serviceResolver)
        {
            this.serviceResolver = serviceResolver;
        }


        public IList<string> Validate(Core.DomainModel.User.User user)
        {
            var rules = serviceResolver.GetAllService<ValidationRule>();
            var result = new List<string>();
            foreach (var rule in rules)
            {
                result.AddRange(rule.Validate(user));
            }
            return result;
        }
    }
}