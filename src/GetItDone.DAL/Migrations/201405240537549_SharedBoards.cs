namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharedBoards : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Schedule_TaskScheduleID", "dbo.TaskSchedules");
            DropForeignKey("dbo.Tasks", "Board_BoardID", "dbo.Boards");
            DropForeignKey("dbo.Sessions", "SessionUser_UserID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Board_BoardID" });
            DropIndex("dbo.Tasks", new[] { "Schedule_TaskScheduleID" });
            DropIndex("dbo.Sessions", new[] { "SessionUser_UserID" });
            RenameColumn(table: "dbo.Tasks", name: "Board_BoardID", newName: "BoardID");
            RenameColumn(table: "dbo.Sessions", name: "SessionUser_UserID", newName: "UserID");
            RenameColumn(table: "dbo.Tasks", name: "Owner_UserID", newName: "Creator_UserID");
            RenameColumn(table: "dbo.Boards", name: "Owner_UserID", newName: "UserID");
            RenameIndex(table: "dbo.Boards", name: "IX_Owner_UserID", newName: "IX_UserID");
            RenameIndex(table: "dbo.Tasks", name: "IX_Owner_UserID", newName: "IX_Creator_UserID");
            CreateTable(
                "dbo.UserBoards",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        BoardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.BoardID })
                .ForeignKey("dbo.Boards", t => t.BoardID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID)
                .Index(t => t.BoardID);
            
            AlterColumn("dbo.Tasks", "BoardID", c => c.Int(nullable: false));
            AlterColumn("dbo.Sessions", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "BoardID");
            CreateIndex("dbo.Sessions", "UserID");
            AddForeignKey("dbo.Tasks", "BoardID", "dbo.Boards", "BoardID", cascadeDelete: false);
            AddForeignKey("dbo.Sessions", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            DropColumn("dbo.Tasks", "Schedule_TaskScheduleID");
            DropTable("dbo.TaskSchedules");
            Sql("INSERT INTO dbo.UserBoards(BoardID, UserID) SELECT BoardID, UserID FROM dbo.Boards"); 
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskSchedules",
                c => new
                    {
                        TaskScheduleID = c.Int(nullable: false, identity: true),
                        Schedule = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskScheduleID);
            
            AddColumn("dbo.Tasks", "Schedule_TaskScheduleID", c => c.Int());
            DropForeignKey("dbo.Sessions", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tasks", "BoardID", "dbo.Boards");
            DropForeignKey("dbo.UserBoards", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserBoards", "BoardID", "dbo.Boards");
            DropIndex("dbo.UserBoards", new[] { "BoardID" });
            DropIndex("dbo.UserBoards", new[] { "UserID" });
            DropIndex("dbo.Sessions", new[] { "UserID" });
            DropIndex("dbo.Tasks", new[] { "BoardID" });
            AlterColumn("dbo.Sessions", "UserID", c => c.Int());
            AlterColumn("dbo.Tasks", "BoardID", c => c.Int());
            DropTable("dbo.UserBoards");
            RenameIndex(table: "dbo.Tasks", name: "IX_Creator_UserID", newName: "IX_Owner_UserID");
            RenameIndex(table: "dbo.Boards", name: "IX_UserID", newName: "IX_Owner_UserID");
            RenameColumn(table: "dbo.Boards", name: "UserID", newName: "Owner_UserID");
            RenameColumn(table: "dbo.Tasks", name: "Creator_UserID", newName: "Owner_UserID");
            RenameColumn(table: "dbo.Sessions", name: "UserID", newName: "SessionUser_UserID");
            RenameColumn(table: "dbo.Tasks", name: "BoardID", newName: "Board_BoardID");
            CreateIndex("dbo.Sessions", "SessionUser_UserID");
            CreateIndex("dbo.Tasks", "Schedule_TaskScheduleID");
            CreateIndex("dbo.Tasks", "Board_BoardID");
            AddForeignKey("dbo.Sessions", "SessionUser_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Tasks", "Board_BoardID", "dbo.Boards", "BoardID");
            AddForeignKey("dbo.Tasks", "Schedule_TaskScheduleID", "dbo.TaskSchedules", "TaskScheduleID");
        }
    }
}
