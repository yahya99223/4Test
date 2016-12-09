using System;
using Core;


namespace IoC
{
    public static class ServiceResolverFactory
    {
        private static IServiceResolver serviceResolver;


        public static IServiceResolver GetServiceResolver(string folder = null)
        {
            if (serviceResolver != null)
                return serviceResolver;

            if (string.IsNullOrEmpty(folder))
                folder = AppDomain.CurrentDomain.BaseDirectory;

            serviceResolver = new ServiceResolver();
            serviceResolver.Initialize(folder);
            return serviceResolver;
        }
    }
}
