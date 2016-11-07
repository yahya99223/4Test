using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using EF.Validation.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EF.Validation.UnitTest
{
    [TestClass]
    public class ValidateDuplicationTests
    {
        private static MyDbContext dbContext;
        private static DbContextTransaction dbContextTransaction;
        private static readonly Guid defaultCompanyId = Guid.Parse("{BD61EA16-DD98-4196-B0D1-727C3BDA2DF9}");
        private static readonly Guid firstUserId = Guid.Parse("{F18086F1-865C-424C-91ED-76D04AE26B74}");
        private static readonly Guid secondUserId = Guid.Parse("{7D9398C3-E2BD-4722-A81A-712585DD2E5D}");
        //private static Guid defaultCompanyId = Guid.Parse("{7BFE442F-9B51-4BCE-9096-BEE1056A179E}");


        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            dbContext = new MyDbContext();
            dbContextTransaction = dbContext.Database.BeginTransaction();
        }


        [ClassCleanup()]
        public static void ClassCleanup()
        {
            dbContextTransaction.Commit();
            dbContextTransaction.Dispose();
            dbContext.Dispose();
        }


        [TestMethod]
        public void Add_First_Person_Should_Success()
        {
            var person = new Person
            {
                Id = firstUserId,
                ManagerId = null,
                CompanyId = defaultCompanyId,
                Name = "user1",
                Email = "user1@domain.com"
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
        }


        [TestMethod]
        public void Add_Second_Person_Should_Success()
        {
            var person = new Person
            {
                Id = secondUserId,
                ManagerId = firstUserId,
                CompanyId = defaultCompanyId,
                Name = "user2",
                Email = "user2@domain.com"
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
        }


        [TestMethod]
        [ExpectedException(typeof(UpdateException))]
        public void Add_Person_With_Duplicated_Name_Should_Throw_Exception()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                ManagerId = firstUserId,
                CompanyId = defaultCompanyId,
                Name = "user2",
                Email = "user3@domain.com"
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
        }


        [TestMethod]
        [ExpectedException(typeof(UpdateException))]
        public void Add_Person_With_Duplicated_Email_Should_Throw_Exception()
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                ManagerId = firstUserId,
                CompanyId = defaultCompanyId,
                Name = "user3",
                Email = "user2@domain.com"
            };

            dbContext.People.Add(person);
            dbContext.SaveChanges();
        }
    }
}