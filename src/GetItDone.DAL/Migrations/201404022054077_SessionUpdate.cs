namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionUpdate : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserSessions", newName: "Sessions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Sessions", newName: "UserSessions");
        }
    }
}
