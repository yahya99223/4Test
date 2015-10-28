using System;
using DomainModel;

namespace Services.Handlers
{
    public class UserBecameInactiveSendEmailHandler : IHandles<UserBecameInactive>
    {
        public void Handle(UserBecameInactive args)
        {
            Console.WriteLine("Sending Email message to {0} to tell him that his account is Inactive.", args.User.UserName);
        }
    }
}
