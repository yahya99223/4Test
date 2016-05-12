using System;
using Castle.MicroKernel.Registration;

namespace IoC
{
    public static class WindsorExtensions
    {
        public static ComponentRegistration<T> NamedRandomly<T>(this ComponentRegistration<T> registration) where T : class
        {
            string name = registration.Implementation.FullName;
            string random = string.Format("{0}{{{1}}}", name, Guid.NewGuid());
            return registration.Named(random);
        }

        public static BasedOnDescriptor NamedRandomly(this BasedOnDescriptor registration)
        {
            return registration.Configure(x => x.NamedRandomly());
        }
    }
}