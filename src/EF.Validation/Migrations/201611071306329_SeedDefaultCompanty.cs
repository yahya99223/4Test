namespace EF.Validation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDefaultCompanty : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Companies]([Id],[Name])VALUES('BD61EA16-DD98-4196-B0D1-727C3BDA2DF9','Fake Company')");
        }
        
        public override void Down()
        {
        }
    }
}
