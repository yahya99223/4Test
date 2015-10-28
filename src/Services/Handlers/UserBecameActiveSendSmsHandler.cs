using System;
using DomainModel;

namespace Services.Handlers
{
    public class UserBecameActiveSendSmsHandler : IHandles<UserBecameActive>
    {
        public void Handle(UserBecameActive args)
        {
            Console.WriteLine("Sending SMS message to {0} to tell him that his account is Active.", args.User.UserName);

        }
    }
}