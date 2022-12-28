namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StartingPrice_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartingPrice_Currency = c.String(),
                        Status = c.String(),
                        Category_Id = c.Int(),
                        OwnerUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Users", t => t.OwnerUser_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.OwnerUser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentCategory = c.Int(nullable: false),
                        ChildCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAuctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product = c.Int(nullable: false),
                        User = c.Int(nullable: false),
                        Price_Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price_Currency = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "OwnerUser_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "OwnerUser_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropTable("dbo.UserAuctions");
            DropTable("dbo.CategoryRelations");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
