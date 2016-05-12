﻿using System;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using Core.DataAccess;
using Core.Model;
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
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestylePerWebRequest());

            container.Register(Classes.FromAssemblyContaining<IUnitOfWork>().Where(t => t.Name.EndsWith("UnitOfWork")).WithService.Base().LifestylePerWebRequest().NamedRandomly());
            container.Register(Classes.FromAssemblyContaining<IUnitOfWork>().Where(t => t.Name.EndsWith("UnitOfWork")).WithService.Base().LifestylePerThread().NamedRandomly());


            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Transient);



            container.Register(Component.For<IHandles<UserCreated>>().ImplementedBy<UserCreatedSleepAsyncHandler>().LifeStyle.Transient);
            container.Register(Component.For<IHandles<UserCreated>>().ImplementedBy<UserCreatedSleep2AsyncHandler>().LifeStyle.Transient);

            container.Register(Component.For<IHandles<UserCreated>>().ImplementedBy<UserCreatedSleepSyncHandler>().LifeStyle.Transient);
        }
    }
}