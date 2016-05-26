using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var date = new DateTime(2016, 8, 4);
            var wantedDate = date.AddYears(-50).AddMonths(-3);
            Console.WriteLine(date.ToString("d"));
            Console.WriteLine(date.ToString("D"));
            Console.WriteLine();
            Console.WriteLine(wantedDate.ToString("d"));
            Console.WriteLine(wantedDate.ToString("D"));
        }


        [TestMethod]
        public void Not_English_Text_Should_Not_Accepted()
        {
            var text = @"Atatürk";
            Assert.IsFalse(Regex.IsMatch(text, @"^[a-zA-Z ]*$"));
        }


        [TestMethod]
        public void English_Text_Should_Be_Accepted()
        {
            var text = @"Ataturk";
            Assert.IsTrue(Regex.IsMatch(text, @"^[a-zA-Z ]*$"));
        }
        [TestMethod]
        public void Arabic_Text_Should_Be_Accepted()
        {
            var text = @"إني أرى رؤوسا ئيء";
            Assert.IsTrue(Regex.IsMatch(text, @"^[\u0600-\u06FF ]*$"));
        }
        [TestMethod]
        public void Mixed_Text_Should__Not_Be_Accepted()
        {
            var text = @"إني أرى Sami رؤوسا ئيء";
            Assert.IsFalse(Regex.IsMatch(text, @"^[\u0600-\u06FF ]*$"));
        }


        [TestMethod]
        public void Valid_Passport_Should_Be_Accepted()
        {
            var text = @"AU23287234";
            Assert.IsTrue(Regex.IsMatch(text, @"^[a-zA-Z0-9]*$"));
        }


        [TestMethod]
        public void Invalid_Passport_Should_Not_Be_Accepted()
        {
            var text = @"AÜ23287234";
            Assert.IsFalse(Regex.IsMatch(text, @"^[a-zA-Z0-9]*$"));
        }
    }
}
