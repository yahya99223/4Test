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
            context.Services.AddOrUpdate(Service.Validation, Service.Normalize, Service.Capitalize);
        }
    }
}
