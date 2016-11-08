using System.Collections.Generic;
using EF.Validation.Model;


namespace EF.Validation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;



    internal sealed class Configuration : DbMigrationsConfiguration<EF.Validation.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


        protected override void Seed(EF.Validation.MyDbContext context)
        {
            /*var defaultCompanyId = Guid.Parse("{BD61EA16-DD98-4196-B0D1-727C3BDA2DF9}");
            var company = new Company()
            {
                Id = defaultCompanyId,
                Name = "Fake Company",
            };

            context.Companies.AddOrUpdate(company);
            context.SaveChanges();*/
        }
    }
}
