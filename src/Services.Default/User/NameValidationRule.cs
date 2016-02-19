using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel.User;
using Core.Modularity;

namespace Services.Default.User
{
    [Name("NameValidationRule")]
    public class NameValidationRule : ValidationRule, IReplaceable
    {
        public override string Name
        {
            get { return "NameValidationRule"; }
        }


        public override IList<string> Validate(Core.DomainModel.User.User user)
        {
            if (user.UserName != "wahid")
            {
                return new List<string> {"Default -> Not allowed"};
            }
            return new List<string>();
        }
    }
}
