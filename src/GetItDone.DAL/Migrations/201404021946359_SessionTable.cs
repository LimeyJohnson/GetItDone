namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Expires = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SessionUser_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.SessionUser_UserID)
                .Index(t => t.SessionUser_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "SessionUser_UserID", "dbo.Users");
            DropIndex("dbo.Sessions", new[] { "SessionUser_UserID" });
            DropTable("dbo.Sessions");
        }
    }
}
