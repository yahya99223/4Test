using OrderManagement.DbModel;

namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OrderManagement.DbModel.OrderManagementDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OrderManagement.DbModel.OrderManagementDbContext context)
        {
            context.Services.AddOrUpdate(
                new Service()
                {
                    Id = Guid.Parse("{5CD76522-E37E-49C2-9D79-20D8C76A4AF8}"),
                    Name = "Capitalize"
                }, new Service()
                {
                    Id = Guid.Parse("{D21B38A1-2B00-497C-BFB8-D577A05B37E0}"),
                    Name = "Normalize Spaces"
                }, new Service()
                {
                    Id = Guid.Parse("{21C75A7E-2426-4DFD-AE09-69912D96290F}"),
                    Name = "Validate"
                });
        }
    }
}
