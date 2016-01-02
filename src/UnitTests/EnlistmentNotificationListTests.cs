using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ConsoleApp.Model;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class EnlistmentNotificationListTests
    {
        private IList<User> inMemoryList;



        [TestInitialize]
        public void Initialize()
        {
            inMemoryList = new EnlistmentNotificationList<User>(new List<User>
            {
                new User(1, "Ahmad1"),
                new User(2, "Ahmad2"),
                new User(3, "Ahmad3"),
            });
        }


        [TestMethod]
        public void Commit_Transaction_Should_Keep_The_New_Changes_In_The_List()
        {
            using (var transaction = new TransactionScope())
            {
                inMemoryList.RemoveAt(1);
                transaction.Complete();
            }
            Assert.IsTrue(inMemoryList.Count == 2);
        }


        [TestMethod]
        public void Rollback_Transaction_Should_Keep_The_Original_Values_In_The_List()
        {
            using (var transaction = new TransactionScope())
            {
                inMemoryList.RemoveAt(1);
            }
            Assert.IsTrue(inMemoryList.Count == 3);
        }


        [TestMethod]
        public void Rollback_Transaction_Should_Keep_The_Original_Objects_Values_In_The_List()
        {
            using (var transaction = new TransactionScope())
            {
                var user = inMemoryList.First(u => u.Id == 1);
                user.Name = "Ahmad Updated";
                inMemoryList.Remove(user);
                inMemoryList.Add(user);
            }
            var userToTest = inMemoryList.First(u => u.Id == 1);
            Assert.IsTrue(userToTest.Name == "Ahmad1");
        }


        [TestMethod]
        public void Commit_Transaction_Should_Keep_The_Updated_Objects_Values_In_The_List()
        {
            using (var transaction = new TransactionScope())
            {
                var user = inMemoryList.First(u => u.Id == 1);
                user.Name = "Ahmad Updated";
                inMemoryList.Remove(user);
                inMemoryList.Add(user);
                transaction.Complete();
            }
            var userToTest = inMemoryList.First(u => u.Id == 1);
            Assert.IsTrue(userToTest.Name == "Ahmad Updated");
        }
    }
}