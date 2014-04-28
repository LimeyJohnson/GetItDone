namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTaskDetailsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Details", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Details", c => c.String(nullable: false));
        }
    }
}
