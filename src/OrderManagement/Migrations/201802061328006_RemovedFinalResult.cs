namespace OrderManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFinalResult : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "FinalResult");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "FinalResult", c => c.String(maxLength: 150));
        }
    }
}
