namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrelloID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boards", "Creator_UserID", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users");
            DropForeignKey("dbo.Boards", "Owner_UserID", "dbo.Users");
            DropIndex("dbo.Boards", new[] { "Creator_UserID" });
            DropIndex("dbo.Tasks", new[] { "Creator_UserID" });
            AddColumn("dbo.Users", "TrelloID", c => c.String());
            AddColumn("dbo.Users", "TrelloToken", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Tasks", "Due", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tasks", "Moved", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tasks", "Creator_UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "Creator_UserID");
            AddForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users", "UserID", cascadeDelete: false);
            DropColumn("dbo.Boards", "UserID");
            DropColumn("dbo.Boards", "Creator_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Boards", "Creator_UserID", c => c.Int());
            AddColumn("dbo.Boards", "UserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Creator_UserID" });
            AlterColumn("dbo.Tasks", "Creator_UserID", c => c.Int());
            AlterColumn("dbo.Tasks", "Moved", c => c.DateTime());
            AlterColumn("dbo.Tasks", "Due", c => c.DateTime());
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.Users", "TrelloToken");
            DropColumn("dbo.Users", "TrelloID");
            CreateIndex("dbo.Tasks", "Creator_UserID");
            CreateIndex("dbo.Boards", "Creator_UserID");
            AddForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Boards", "Creator_UserID", "dbo.Users", "UserID");
        }
    }
}
