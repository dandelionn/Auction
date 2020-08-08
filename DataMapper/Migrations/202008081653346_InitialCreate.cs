namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 100),
                        CurrencyName = c.String(nullable: false),
                        BeginPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Seller_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sellers", t => t.Seller_Id, cascadeDelete: true)
                .Index(t => t.Seller_Id);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Auction_Id = c.Int(nullable: false),
                        Bidder_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.Auction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bidders", t => t.Bidder_Id, cascadeDelete: true)
                .Index(t => t.Auction_Id)
                .Index(t => t.Bidder_Id);
            
            CreateTable(
                "dbo.Bidders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 100),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ProductAuctions",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Auction_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Auction_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Auctions", t => t.Auction_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Auction_Id);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Product_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductSellers",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Seller_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Seller_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.Seller_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Seller_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "Seller_Id", "dbo.Sellers");
            DropForeignKey("dbo.Bids", "Bidder_Id", "dbo.Bidders");
            DropForeignKey("dbo.Bidders", "Id", "dbo.People");
            DropForeignKey("dbo.ProductSellers", "Seller_Id", "dbo.Sellers");
            DropForeignKey("dbo.ProductSellers", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductAuctions", "Auction_Id", "dbo.Auctions");
            DropForeignKey("dbo.ProductAuctions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Sellers", "Id", "dbo.People");
            DropForeignKey("dbo.Bids", "Auction_Id", "dbo.Auctions");
            DropIndex("dbo.ProductSellers", new[] { "Seller_Id" });
            DropIndex("dbo.ProductSellers", new[] { "Product_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_Id" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_Id" });
            DropIndex("dbo.ProductAuctions", new[] { "Auction_Id" });
            DropIndex("dbo.ProductAuctions", new[] { "Product_Id" });
            DropIndex("dbo.Categories", new[] { "Category_Id" });
            DropIndex("dbo.Sellers", new[] { "Id" });
            DropIndex("dbo.Bidders", new[] { "Id" });
            DropIndex("dbo.Bids", new[] { "Bidder_Id" });
            DropIndex("dbo.Bids", new[] { "Auction_Id" });
            DropIndex("dbo.Auctions", new[] { "Seller_Id" });
            DropTable("dbo.ProductSellers");
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.ProductAuctions");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Sellers");
            DropTable("dbo.People");
            DropTable("dbo.Bidders");
            DropTable("dbo.Bids");
            DropTable("dbo.Auctions");
        }
    }
}
