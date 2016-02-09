using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class NestedArrayTests
    {
        [TestMethod]
        public void Query_Empty_Nested_Array_By_SelectMany()
        {
            var main = new MainClass();
            var step = main.SubClassOnes.SelectMany(s => s.SubClassTwos).FirstOrDefault();
            Assert.IsNull(step);
        }


        [TestMethod]
        public void Query_Empty_Nested_Array_By_SelectMany_With_Filter_In_FirstOrDefault()
        {
            var main = new MainClass();
            var step = main.SubClassOnes.SelectMany(s => s.SubClassTwos).FirstOrDefault(s => s.Id == Guid.NewGuid());
            Assert.IsNull(step);
        }


        [TestMethod]
        public void Query_First_Level_Nested_Array_By_SelectMany_With_Filter_In_FirstOrDefault()
        {
            var main = new MainClass(new List<SubClassOne> {new SubClassOne()});
            var step = main.SubClassOnes.SelectMany(s => s.SubClassTwos).FirstOrDefault(s => s.Id == Guid.NewGuid());
            Assert.IsNull(step);
        }


        [TestMethod]
        public void Query_First_Level_Nested_Array_By_SelectMany_With_Filter_In_FirstOrDefault_2()
        {
            var main = new MainClass(new List<SubClassOne> {new SubClassOne()});
            var step = main.list.SelectMany(s => s.SubClassTwos).FirstOrDefault(s => s.Id == Guid.NewGuid());
            Assert.IsNull(step);
        }
    }
}