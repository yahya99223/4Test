using System.Configuration;
using Microsoft.AspNet.SignalR;
using Owin;

namespace Application.API
{
    public static class SignalRConfig
    {
        public static void Register(IAppBuilder app)
        {
            // Branch the pipeline here for requests that start with "/signalr"

            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
          
                var hubConfiguration = new Microsoft.AspNet.SignalR.HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    EnableJSONP = true,
                    EnableJavaScriptProxies = true,
                    EnableDetailedErrors = true,
                };

                // Create JsonSerializer with StringEnumConverter. and Register the serializer.
                var serializer = new Newtonsoft.Json.JsonSerializer();

                serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                //serializer.ContractResolver = new SignalRCamelCasePropertyNamesContractResolver();

                GlobalHost.DependencyResolver.Register(typeof(Newtonsoft.Json.JsonSerializer), () => serializer);

                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}