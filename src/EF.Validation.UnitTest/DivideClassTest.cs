using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EF.Validation.UnitTest
{
    [TestClass()]
    public class DivideClassTest
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Console.WriteLine("Assembly Init");
        }


        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("ClassInit");
        }


        [TestInitialize()]
        public void Initialize()
        {
            Console.WriteLine("TestMethodInit");
        }


        [TestCleanup()]
        public void Cleanup()
        {
            Console.WriteLine("TestMethodCleanup");
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            Console.WriteLine("ClassCleanup");
        }


        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            Console.WriteLine("AssemblyCleanup");
        }


        [TestMethod()]
        [ExpectedException(typeof(System.DivideByZeroException))]
        public void DivideMethodTest()
        {
            var target = new DivideClass();
            int a = 0;
            int actual;
            actual = target.DivideMethod(a);
        }



        public class DivideClass
        {
            public int DivideMethod(int a)
            {
                return 2/a;
            }
        }
    }
}