namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatagoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        CatagoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ColorCode = c.String(),
                        Owner_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CatagoryID)
                .ForeignKey("dbo.Users", t => t.Owner_UserID, cascadeDelete: true)
                .Index(t => t.Owner_UserID);
            
            AddColumn("dbo.Tasks", "Catagory_CatagoryID", c => c.Int());
            CreateIndex("dbo.Tasks", "Catagory_CatagoryID");
            AddForeignKey("dbo.Tasks", "Catagory_CatagoryID", "dbo.Catagories", "CatagoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Catagories", "Owner_UserID", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Catagory_CatagoryID", "dbo.Catagories");
            DropIndex("dbo.Tasks", new[] { "Catagory_CatagoryID" });
            DropIndex("dbo.Catagories", new[] { "Owner_UserID" });
            DropColumn("dbo.Tasks", "Catagory_CatagoryID");
            DropTable("dbo.Catagories");
        }
    }
}
