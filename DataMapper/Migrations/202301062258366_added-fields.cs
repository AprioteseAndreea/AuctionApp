namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "BirthDate", c => c.String());
            AddColumn("dbo.Users", "Score", c => c.Double());
            AddColumn("dbo.CategoryRelations", "ChildCategory_Id", c => c.Int());
            AddColumn("dbo.CategoryRelations", "ParentCategory_Id", c => c.Int());
            AddColumn("dbo.UserAuctions", "Product_Id", c => c.Int());
            AddColumn("dbo.UserAuctions", "User_Id", c => c.Int());
            AlterColumn("dbo.Configurations", "InitialScore", c => c.Double(nullable: false));
            AlterColumn("dbo.Configurations", "MinScore", c => c.Double(nullable: false));
            CreateIndex("dbo.CategoryRelations", "ChildCategory_Id");
            CreateIndex("dbo.CategoryRelations", "ParentCategory_Id");
            CreateIndex("dbo.UserAuctions", "Product_Id");
            CreateIndex("dbo.UserAuctions", "User_Id");
            AddForeignKey("dbo.CategoryRelations", "ChildCategory_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.CategoryRelations", "ParentCategory_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.UserAuctions", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.UserAuctions", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.CategoryRelations", "ParentCategory");
            DropColumn("dbo.CategoryRelations", "ChildCategory");
            DropColumn("dbo.UserAuctions", "Product");
            DropColumn("dbo.UserAuctions", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAuctions", "User", c => c.Int(nullable: false));
            AddColumn("dbo.UserAuctions", "Product", c => c.Int(nullable: false));
            AddColumn("dbo.CategoryRelations", "ChildCategory", c => c.Int(nullable: false));
            AddColumn("dbo.CategoryRelations", "ParentCategory", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserAuctions", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserAuctions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryRelations", "ParentCategory_Id", "dbo.Categories");
            DropForeignKey("dbo.CategoryRelations", "ChildCategory_Id", "dbo.Categories");
            DropIndex("dbo.UserAuctions", new[] { "User_Id" });
            DropIndex("dbo.UserAuctions", new[] { "Product_Id" });
            DropIndex("dbo.CategoryRelations", new[] { "ParentCategory_Id" });
            DropIndex("dbo.CategoryRelations", new[] { "ChildCategory_Id" });
            AlterColumn("dbo.Configurations", "MinScore", c => c.Int(nullable: false));
            AlterColumn("dbo.Configurations", "InitialScore", c => c.Int(nullable: false));
            DropColumn("dbo.UserAuctions", "User_Id");
            DropColumn("dbo.UserAuctions", "Product_Id");
            DropColumn("dbo.CategoryRelations", "ParentCategory_Id");
            DropColumn("dbo.CategoryRelations", "ChildCategory_Id");
            DropColumn("dbo.Users", "Score");
            DropColumn("dbo.Users", "BirthDate");
            DropColumn("dbo.Users", "Email");
        }
    }
}
