namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUserTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Details = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Due = c.DateTime(),
                        Priority = c.Int(),
                        Owner_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.Users", t => t.Owner_UserID, cascadeDelete: true)
                .Index(t => t.Owner_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Owner_UserID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Owner_UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
        }
    }
}
