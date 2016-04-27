using System;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.DataAccess;
using Core.Services;
using Microsoft.Owin.Security.OAuth;

namespace IDScan.OnboardingSuite.Shared.WindsorInstallers.ApplicationAPIInstaller
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<System.Web.Http.Dependencies.IDependencyResolver>().ImplementedBy<WindsorHttpDependencyResolver>());
            container.Register(Classes.FromAssemblyNamed("ApplicationAPI").BasedOn<IHttpController>().LifestyleScoped());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Transient);
        }
    }
}