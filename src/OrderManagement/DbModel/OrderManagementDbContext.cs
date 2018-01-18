using System.Data.Entity;

namespace OrderManagement.DbModel
{
    public class OrderManagementDbContext : DbContext
    {
        public OrderManagementDbContext() : base("OrderManagement")
        {

        }

        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Service> Services { get; set; }
        public IDbSet<ProcessResult> ProcessResults { get; set; }
    }
}