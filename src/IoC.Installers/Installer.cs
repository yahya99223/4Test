using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleApp.Handlers;
using Core;
using Core.DataAccessContracts;
using Core.DomainModel;
using Core.DomainModel.User;
using Core.ServicesContracts;
using DataAccess;
using Default=Services.Default;

namespace IoC
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IServiceResolver>().ImplementedBy<ServiceResolver>().LifestyleSingleton());

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleSingleton());

            var interfaces = typeof (IUserService).Assembly.GetTypes().Where(t => t.IsInterface && t.IsPublic).ToList();
            
            container.Register(Component.For<IUserService>().ImplementedBy<Default.User.UserService>().Named("Default.User.UserService"));

            container.Register(Component.For<IUserService>().ImplementedBy<Default.User.UserService>().Named("Default.User.UserService"));

            container.Register(Component.For<IJourneyService>().ImplementedBy<Default.Journey.JourneyService>());

            container.Register(Component.For(typeof (IHandles<AddedModel<User>>)).ImplementedBy(typeof (UserAddedShowMessageHandler)));

            container.Register(Component.For<IHandles<UserBecameActive>>().ImplementedBy<Default.User.UserBecameActiveSendEmailHandler>());

            container.Register(Component.For<IHandles<UserBecameInactive>>().ImplementedBy<Default.User.UserBecameInactiveSendEmailHandler>());

            container.Register(Component.For<IHandles<UserBecameActive>>().ImplementedBy<Default.User.UserBecameActiveSendSmsHandler>());

            container.Register(Component.For<IHandles<UserBecameInactive>>().ImplementedBy<Default.User.UserBecameInactiveSendSmsHandler>());
        }
    }
}
