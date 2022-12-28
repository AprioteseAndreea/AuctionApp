namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
