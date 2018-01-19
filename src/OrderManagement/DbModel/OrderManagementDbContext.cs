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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(x => x.ProcessResults)
                .WithRequired(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()
                .Property(x => x.Status)
                .HasMaxLength(100);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Services)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("OrderId");
                    m.MapRightKey("ServiceId");
                    m.ToTable("OrdersServices");
                });

            modelBuilder.Entity<ProcessResult>()
                .HasRequired(x => x.Service)
                .WithMany()
                .HasForeignKey(x => x.ServiceId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}