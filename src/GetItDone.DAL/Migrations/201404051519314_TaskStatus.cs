namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Completed", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Tasks", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Tasks", "Done");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Done", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tasks", "Status");
            DropColumn("dbo.Tasks", "Duration");
            DropColumn("dbo.Tasks", "Completed");
        }
    }
}
