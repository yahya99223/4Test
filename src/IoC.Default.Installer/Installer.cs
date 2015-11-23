using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using Core.DataAccessContracts;
using Core.DomainModel;
using Core.DomainModel.User;
using Core.ServicesContracts;
using DataAccess;

namespace IoC.Default.Installer
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("ConsoleApp").BasedOn<IHandles<AddedModel<User>>>());

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleSingleton());            
            
            container.Register(Component.For<IUserService>().ImplementedBy<Services.Default.User.UserService>().Named("Default.User.UserService"));

            container.Register(Component.For<IJourneyService>().ImplementedBy<Services.Default.Journey.JourneyService>());

            //container.Register(Component.For(typeof (IHandles<AddedModel<User>>)).ImplementedBy(typeof (UserAddedShowMessageHandler)));

            container.Register(Component.For<IHandles<UserBecameActive>>().ImplementedBy<Services.Default.User.UserBecameActiveSendEmailHandler>());

            container.Register(Component.For<IHandles<UserBecameInactive>>().ImplementedBy<Services.Default.User.UserBecameInactiveSendEmailHandler>());

            container.Register(Component.For<IHandles<UserBecameActive>>().ImplementedBy<Services.Default.User.UserBecameActiveSendSmsHandler>());

            container.Register(Component.For<IHandles<UserBecameInactive>>().ImplementedBy<Services.Default.User.UserBecameInactiveSendSmsHandler>());
        }
    }
}
