namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restorestatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Status", c => c.String());
            DropColumn("dbo.UserAuctions", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAuctions", "Status", c => c.String());
            DropColumn("dbo.Products", "Status");
        }
    }
}
