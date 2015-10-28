using System;
using Core;

namespace ConsoleApp.Handlers
{
    public class UserAddedShowMessageHandler : IHandles<UserAdded>
    {
        public void Handle(UserAdded args)
        {
            Console.WriteLine("Welcome {0}. ^_^ ", args.User.UserName);
        }
    }
}