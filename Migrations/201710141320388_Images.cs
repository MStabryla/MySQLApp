namespace MySQLApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("OrderProducts");
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(),
                        Name = c.String(unicode: false),
                        ConType = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Rating", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "Image_Id", c => c.Int());
            AddPrimaryKey("OrderProducts", new[] { "Order_Id", "Product_Id" });
            CreateIndex("dbo.Products", "Image_Id");
            AddForeignKey("dbo.Products", "Image_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Image_Id", "dbo.Images");
            DropIndex("dbo.Products", new[] { "Image_Id" });
            DropPrimaryKey("dbo.OrderProducts");
            DropColumn("dbo.Products", "Image_Id");
            DropColumn("dbo.Products", "Rating");
            DropTable("dbo.Images");
            AddPrimaryKey("dbo.OrderProducts", new[] { "Product_Id", "Order_Id" });
            RenameTable(name: "dbo.OrderProducts", newName: "ProductOrders");
        }
    }
}
