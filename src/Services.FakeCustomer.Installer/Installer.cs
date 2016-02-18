﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using Core.DomainModel.User;
using Core.ServicesContracts;
using Services.FakeCustomer.User;

namespace Services.FakeCustomer.Installer
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().Named("IUserService" + Statics.CurrentBusinessCode));
            container.Register(Component.For<ValidationRule>().ImplementedBy<NameValidationRule>());
            container.Register(Component.For<ValidationRule>().ImplementedBy<EmailValidationRule>());
            //container.Register(Classes.FromAssembly(typeof(NameValidationRule).Assembly).BasedOn<ValidationRule>().LifestyleTransient());
        }
    }
}
