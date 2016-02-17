using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.DataAccessContracts;
using Core.ServicesContracts;
using DataAccess;
using Services.Default.User;

namespace Services.Default.Installer
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestyleSingleton());

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifestyleTransient());
        }
    }
}
