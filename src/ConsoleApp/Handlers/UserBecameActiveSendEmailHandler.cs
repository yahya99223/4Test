using System;
using Core;
using Core.DomainModel;
using Core.DomainModel.User;

namespace ConsoleApp.Handlers
{
    public class UserAddedShowMessageHandler : IHandles<AddedModel<User>>
    {
        public void Handle(AddedModel<User> args)
        {
            Console.WriteLine("Welcome {0}. ^_^ ", args.Model.UserName);
            throw new NotImplementedException();
        }
    }
}