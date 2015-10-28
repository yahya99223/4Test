using System;
using DomainModel;

namespace Services.Handlers
{
    public class UserBecameInactiveSendSmsHandler : IHandles<UserBecameInactive>
    {
        public void Handle(UserBecameInactive args)
        {
            Console.WriteLine("Sending SMS message to {0} to tell him that his account is Inactive.", args.User.UserName);
        }
    }
}