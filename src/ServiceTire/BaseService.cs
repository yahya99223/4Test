using Helpers;

namespace ServiceTire
{
    public abstract class BaseService<T>
    {
        public BaseService(string serviceName)
        {
            this.Log().Info("1 - '" + serviceName + "' Initialized");
            this.LogFor(typeof (T)).Info("2 - '" + serviceName + "' Initialized");
        }
    }
}