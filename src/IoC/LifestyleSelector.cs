using System;
using System.Linq;
using System.Web;
using Castle.Core;
using Castle.MicroKernel;

namespace IoC
{
    public class LifestyleSelector : IHandlerSelector
    {
        public bool HasOpinionAbout(string key, Type service)
        {
            return service != typeof(object); // for some reason, Castle passes typeof(object) if the service type is null.
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            if (handlers.Length == 2 && handlers.Any(x => x.ComponentModel.LifestyleType == LifestyleType.PerWebRequest))
            {
                if (HttpContext.Current == null)
                {
                    return handlers.Single(x => x.ComponentModel.LifestyleType != LifestyleType.PerWebRequest);
                }
                else
                {
                    return handlers.Single(x => x.ComponentModel.LifestyleType == LifestyleType.PerWebRequest);
                }
            }
            return null; // we don't have an opinion in this case.
        }
    }
}