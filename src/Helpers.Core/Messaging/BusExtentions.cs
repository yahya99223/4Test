using System;
using System.Threading.Tasks;
using MassTransit;

namespace Helpers.Core
{
    public static class BusExtentions
    {
        public static async Task<TRes> GetCommandResponseAsync<TCom, TRes>(this IBusControl bus, TCom command, Uri serviceAddress, int timeOut) where TCom : class where TRes : class
        {
            var client = bus.CreateRequestClient<TCom, TRes>(serviceAddress, TimeSpan.FromSeconds(timeOut));
            return await client.Request(command);
        }
    }
}