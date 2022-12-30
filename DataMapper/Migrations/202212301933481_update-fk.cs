namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "OwnerUser_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            AddForeignKey("dbo.Products", "OwnerUser_Id", "dbo.Users", cascadeDelete: true);
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", cascadeDelete: true);


        }

        public override void Down()
        {
        }
    }
}
