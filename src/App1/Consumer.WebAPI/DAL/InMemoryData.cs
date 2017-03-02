using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Messaging.Messages;


namespace Consumer.WebAPI.DAL
{
    public static class InMemoryData
    {
        static InMemoryData()
        {
            Letters = new List<ILetter>();
        }


        public static IList<ILetter> Letters { get; set; }
    }
}