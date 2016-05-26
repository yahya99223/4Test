using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebLoadTest
{
    [TestClass]
    public class UnitTest1
    {
        private Random rnd = new Random();

        [TestMethod]
        public void TestMethod1()
        {
            var val = rnd.Next(2000000, 200000000);
            for (int i = 0; i < val; i++)
            {
                Thread.Sleep(100);
                var x = i/val*213 ^ 12312;
            }
        }
    }
}
