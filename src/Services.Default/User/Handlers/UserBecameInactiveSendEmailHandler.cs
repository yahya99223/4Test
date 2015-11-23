using System;
using Core;
using Core.DomainModel.User;

namespace Services.Default.User
{
    public class UserBecameInactiveSendEmailHandler : IHandles<UserBecameInactive>
    {
        public void Handle(UserBecameInactive args)
        {
            Console.WriteLine("Sending Email message to {0} to tell him that his account is Inactive.", args.User.UserName);
        }
    }
}
