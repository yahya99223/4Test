using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using Core.DataAccess;
using Core.Model;
using Core.Services;

namespace IDScan.OnboardingSuite.Shared.WindsorInstallers.ApplicationAPIInstaller
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<System.Web.Http.Dependencies.IDependencyResolver>().ImplementedBy<WindsorHttpDependencyResolver>());
            container.Register(Classes.FromAssemblyNamed("ApplicationAPI").BasedOn<IHttpController>().LifestyleScoped());

            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestyleScoped());


            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Transient);



            container.Register(Component.For<IBackgroundHandles<UserCreated>>().ImplementedBy<UserCreated_Async1Handler>().LifeStyle.Transient);
            container.Register(Component.For<IBackgroundHandles<UserCreated>>().ImplementedBy<UserCreated_Async2Handler>().LifeStyle.Transient);
            container.Register(Component.For<IHandles<UserCreated>>().ImplementedBy<UserCreated_SyncHandler>().LifeStyle.Transient);
        }
    }
}