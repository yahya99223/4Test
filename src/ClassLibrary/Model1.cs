namespace ClassLibrary
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ApplicationConfigurationItem> ApplicationConfigurationItems { get; set; }
        public virtual DbSet<ApplicationConfigurationItemValue> ApplicationConfigurationItemValues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationConfigurationItem>()
                .HasMany(e => e.ApplicationConfigurationItemValues)
                .WithRequired()
                .HasForeignKey(e => e.ItemKey)
                .WillCascadeOnDelete(false);
        }
    }
}
