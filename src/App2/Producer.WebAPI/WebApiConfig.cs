using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace Producer.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            routeConfig(config);

            formattersConfig(config);

            filtersConfig(config);
        }


        private static void routeConfig(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
        }


        private static void formattersConfig(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }


        private static void filtersConfig(HttpConfiguration config)
        {
        }

    }
}