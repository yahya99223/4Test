using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Consumer.WebAPI.Model;


namespace Consumer.WebAPI.DAL
{
    public static class InMemoryData
    {
        static InMemoryData()
        {
            Letters = new List<Letter>();
        }


        public static IList<Letter> Letters { get; set; }
    }
}