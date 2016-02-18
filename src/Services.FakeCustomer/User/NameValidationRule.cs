using System.Collections.Generic;
using Core.DomainModel.User;
using Core.Modularity;

namespace Services.FakeCustomer.User
{
    [Replace]
    public class NameValidationRule : ValidationRule
    {
        public override string Name
        {
            get { return "NameValidationRule"; }
        }


        public override IList<string> Validate(Core.DomainModel.User.User user)
        {
            if (user.UserName != "wahid")
            {
                return new List<string> {"Fake -> Not Accepted"};
            }
            return new List<string>();
        }
    }



    public class EmailValidationRule : ValidationRule
    {
        public override string Name
        {
            get { return "EmailValidationRule"; }
        }


        public override IList<string> Validate(Core.DomainModel.User.User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new List<string> {"Fake -> Email is required"};
            }
            return new List<string>();
        }
    }
}