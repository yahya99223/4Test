using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoadTest
{
    [TestClass]
    public class GetUnitTest
    {
        [TestMethod]
        public void Get()
        {
            using (var client = new HttpClient())
            {
                var result = client.GetAsync("http://localhost/LoadTest/api/Get/Test").Result;
                Console.WriteLine(result);
            }
        }
    }
}
