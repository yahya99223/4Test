using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using EF.Validation.Model;
using NUnit.Framework;


namespace EF.Validation.UnitTest
{
    [TestFixture]
    public class ValidateDuplicationTests
    {
        private MyDbContext dbContext;
        private DbContextTransaction dbContextTransaction;
        private readonly Guid defaultCompanyId = Guid.Parse("{BD61EA16-DD98-4196-B0D1-727C3BDA2DF9}");
        private readonly Guid firstUserId = Guid.Parse("{F18086F1-865C-424C-91ED-76D04AE26B74}");
        private readonly Guid secondUserId = Guid.Parse("{7D9398C3-E2BD-4722-A81A-712585DD2E5D}");


        [OneTimeSetUp]
        public void ClassInit()
        {
            dbContext = new MyDbContext();
            dbContextTransaction = dbContext.Database.BeginTransaction();
        }


        [OneTimeTearDown]
        public void ClassCleanup()
        {
            dbContextTransaction.Rollback();
            dbContextTransaction.Dispose();
            dbContext.Dispose();
        }


        [Test, Order(1)]
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


        [Test, Order(2)]
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


        [Test, Order(3)]
        public void Update_Person_Should_Pass()
        {
            var person = dbContext.People.First(p => p.Id == secondUserId);
            person.Name = "user2_updated";
            dbContext.SaveChanges();
        }


        [Test, Order(4)]
        public void Add_Person_With_Duplicated_Name_Should_Throw_Exception()
        {
            Assert.Throws<DbEntityValidationException>(() =>
                                                       {
                                                           var person = new Person
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               ManagerId = firstUserId,
                                                               CompanyId = defaultCompanyId,
                                                               Name = "user2_updated",
                                                               Email = "user3@domain.com"
                                                           };

                                                           dbContext.People.Add(person);
                                                           dbContext.SaveChanges();
                                                       }
            );
        }


        [Test, Order(5)]
        public void Add_Person_With_Duplicated_Email_Should_Throw_Exception()
        {
            Assert.Throws<DbEntityValidationException>(() =>
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
            );
        }


        [Test, Order(6)]
        public void Update_Person_With_Duplicated_Name_With_Another_Person_Should_Throw_Exception()
        {
            Assert.Throws<DbEntityValidationException>(() =>
                                                       {
                                                           var person = dbContext.People.First(p => p.Id == secondUserId);
                                                           person.Name = "user1";
                                                           dbContext.SaveChanges();
                                                       }
            );
        }
    }
}