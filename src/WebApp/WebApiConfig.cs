using System;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace WebApp
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
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IPAddressConverter());
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IPEndPointConverter());
        }


        private static void filtersConfig(HttpConfiguration config)
        {
            //config.Services.Clear(typeof(IExceptionHandler));
            //config.Filters.Add(new AuthorizeAttribute());
        }
    }

    class IPAddressConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPAddress));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IPAddress ip = (IPAddress)value;
            writer.WriteValue(ip.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            return IPAddress.Parse(token.Value<string>());
        }
    }

    class IPEndPointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPEndPoint));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IPEndPoint ep = (IPEndPoint)value;
            writer.WriteStartObject();
            writer.WritePropertyName("Address");
            serializer.Serialize(writer, ep.Address);
            writer.WritePropertyName("Port");
            writer.WriteValue(ep.Port);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            IPAddress address = jo["Address"].ToObject<IPAddress>(serializer);
            int port = jo["Port"].Value<int>();
            return new IPEndPoint(address, port);
        }
    }
}