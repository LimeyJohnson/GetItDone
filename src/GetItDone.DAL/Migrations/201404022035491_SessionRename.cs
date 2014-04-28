namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sessions", newName: "UserSessions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserSessions", newName: "Sessions");
        }
    }
}
