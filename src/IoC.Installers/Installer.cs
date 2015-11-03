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
using Services;
using Services.Handlers;

namespace IoC
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IServiceResolver>().ImplementedBy<ServiceResolver>().LifestyleSingleton());

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleSingleton());

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>());

            container.Register(Component.For(typeof (IHandles<AddedModel<User>>)).ImplementedBy(typeof (UserAddedShowMessageHandler)));

            container.Register(Component.For<IHandles<UserBecameActive>>().ImplementedBy<UserBecameActiveSendEmailHandler>());

            container.Register(Component.For<IHandles<UserBecameInactive>>().ImplementedBy<UserBecameInactiveSendEmailHandler>());

            container.Register(Component.For<IHandles<UserBecameActive>>().ImplementedBy<UserBecameActiveSendSmsHandler>());

            container.Register(Component.For<IHandles<UserBecameInactive>>().ImplementedBy<UserBecameInactiveSendSmsHandler>());
        }
    }
}
