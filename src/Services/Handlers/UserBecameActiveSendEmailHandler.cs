using System;
using Core;
using Core.DomainModel.User;

namespace Services.Handlers
{
    public class UserBecameActiveSendEmailHandler : IHandles<UserBecameActive>
    {
        public void Handle(UserBecameActive args)
        {
            Console.WriteLine("Sending Email message to {0} to tell him that his account is Active.", args.User.UserName);
        }
    }
}