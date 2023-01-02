﻿namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuscolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAuctions", "Status", c => c.String());
            DropColumn("dbo.Products", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Status", c => c.String());
            DropColumn("dbo.UserAuctions", "Status");
        }
    }
}
