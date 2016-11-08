using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
        private readonly Guid defaultCountryId = Guid.Parse("{64121657-8342-4575-A760-09C9A73775CD}");
        private readonly Guid mersinCityId = Guid.Parse("{0511EE31-A509-4635-8BFB-25092F5F5464}");
        private readonly Guid bursaCityId = Guid.Parse("{BA1255D5-C451-448F-967B-3113EA805D72}");
        private readonly Guid istanbulCityId = Guid.Parse("{52EBAF89-57F7-4780-B8CF-FAD63CF76E3C}");
        private readonly Guid firstUserId = Guid.Parse("{F18086F1-865C-424C-91ED-76D04AE26B74}");
        private readonly Guid secondUserId = Guid.Parse("{7D9398C3-E2BD-4722-A81A-712585DD2E5D}");


        [SetUp]
        public void Setup()
        {
            dbContext = new MyDbContext();
            //dbContextTransaction = dbContext.Database.BeginTransaction();
        }


        [TearDown]
        public void Cleanup()
        {
            //dbContextTransaction.Rollback();
            //dbContextTransaction.Dispose();
            dbContext.Dispose();
        }


        [OneTimeTearDown]
        public void ClassCleanup()
        {
            // MyDbContext.ResetDb();
        }


        [Test, Order(1)]
        public void A_Add_First_Person_Should_Success()
        {
            try
            {
                var person = new Person
                {
                    Id = firstUserId,
                    ManagerId = null,
                    CompanyId = defaultCompanyId,
                    Name = "user1",
                    Email = "user1@domain.com",
                    Offices = new List<Office>
                    {
                        new Office
                        {
                            CountryId = defaultCountryId,
                            CityId = istanbulCityId,
                            PersonId = firstUserId,
                            Name = "Istanbul Office"
                        },

                        new Office
                        {
                            CountryId = defaultCountryId,
                            CityId = bursaCityId,
                            PersonId = secondUserId,
                            Name = "Bursa Office"
                        }
                    }
                };

                dbContext.People.Add(person);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var errors = dbContext.GetValidationErrors();
                Console.WriteLine(ex);
                throw;
            }
        }


        [Test, Order(2)]
        public void B_Add_Second_Person_Should_Success()
        {
            try
            {
                var person = new Person
                {
                    Id = secondUserId,
                    ManagerId = firstUserId,
                    CompanyId = defaultCompanyId,
                    Name = "user2",
                    Email = "user2@domain.com",
                    Offices = new List<Office>
                    {
                        new Office
                        {
                            CountryId = defaultCountryId,
                            CityId = mersinCityId,
                            PersonId = secondUserId,
                            Name = "Mersin Office"
                        }
                    }
                };

                dbContext.People.Add(person);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var errors = dbContext.GetValidationErrors();
                Console.WriteLine(ex);
                throw;
            }
        }


        [Test, Order(3)]
        public void C_Update_Person_Should_Pass()
        {
            try
            {
                var person = dbContext.People.First(p => p.Id == secondUserId);
                person.Name = "user2_updated";
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        [Test, Order(4)]
        public void D_Add_Person_With_Duplicated_Name_Should_Throw_Exception()
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
        public void E_Add_Person_With_Duplicated_Email_Should_Throw_Exception()
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
        public void F_Update_Person_With_Duplicated_Name_With_Another_Person_Should_Throw_Exception()
        {
            Assert.Throws<DbEntityValidationException>(() =>
                                                       {
                                                           var person = dbContext.People.First(p => p.Id == secondUserId);
                                                           person.Name = "user1";
                                                           dbContext.SaveChanges();
                                                       }
            );
        }


        [Test, Order(7)]
        public void G_Update_Offices_With_Duplicated_Name_With_Another_Person_Should_Throw_Exception()
        {
            Assert.Throws<DbEntityValidationException>(() =>
                                                       {
                                                           try
                                                           {
                                                               var person = dbContext.People.Include(p => p.Offices).First(p => p.Id == secondUserId);
                                                               var office = person.Offices.First();
                                                               office.Name = "Bursa Office";
                                                               dbContext.SaveChanges();
                                                           }
                                                           catch (DbEntityValidationException dbEx)
                                                           {
                                                               foreach (var validationErrors in dbEx.EntityValidationErrors)
                                                               {
                                                                   foreach (var validationError in validationErrors.ValidationErrors)
                                                                   {
                                                                       Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                                                                   }
                                                               }
                                                               throw;
                                                           }
                                                           catch (Exception ex)
                                                           {
                                                               Console.WriteLine(ex);
                                                               throw;
                                                           }
                                                       }
            );
        }


        [Test, Order(8)]
        public void H_RestDatabase()
        {
            MyDbContext.ResetDb();
        }
    }
}