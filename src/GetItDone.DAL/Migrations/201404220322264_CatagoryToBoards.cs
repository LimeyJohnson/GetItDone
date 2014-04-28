namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatagoryToBoards : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Catagory_CatagoryID", "dbo.Catagories");
            DropForeignKey("dbo.Catagories", "Owner_UserID", "dbo.Users");
            DropIndex("dbo.Catagories", new[] { "Owner_UserID" });
            DropIndex("dbo.Tasks", new[] { "Catagory_CatagoryID" });
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        BoardID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ColorCode = c.String(),
                        Owner_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BoardID)
                .ForeignKey("dbo.Users", t => t.Owner_UserID, cascadeDelete: true)
                .Index(t => t.Owner_UserID);
            
            AddColumn("dbo.Tasks", "Board_BoardID", c => c.Int());
            CreateIndex("dbo.Tasks", "Board_BoardID");
            AddForeignKey("dbo.Tasks", "Board_BoardID", "dbo.Boards", "BoardID");
            DropColumn("dbo.Tasks", "Status");
            DropColumn("dbo.Tasks", "Catagory_CatagoryID");
            DropTable("dbo.Catagories");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.CatagoryID);
            
            AddColumn("dbo.Tasks", "Catagory_CatagoryID", c => c.Int());
            AddColumn("dbo.Tasks", "Status", c => c.Int(nullable: false));
            DropForeignKey("dbo.Boards", "Owner_UserID", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Board_BoardID", "dbo.Boards");
            DropIndex("dbo.Tasks", new[] { "Board_BoardID" });
            DropIndex("dbo.Boards", new[] { "Owner_UserID" });
            DropColumn("dbo.Tasks", "Board_BoardID");
            DropTable("dbo.Boards");
            CreateIndex("dbo.Tasks", "Catagory_CatagoryID");
            CreateIndex("dbo.Catagories", "Owner_UserID");
            AddForeignKey("dbo.Catagories", "Owner_UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Tasks", "Catagory_CatagoryID", "dbo.Catagories", "CatagoryID");
        }
    }
}
