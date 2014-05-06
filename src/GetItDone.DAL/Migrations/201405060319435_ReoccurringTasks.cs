namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReoccurringTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "ReoccurringSchedule", c => c.Int());
            AddColumn("dbo.Tasks", "ReoccurringParent_TaskID", c => c.Int());
            CreateIndex("dbo.Tasks", "ReoccurringParent_TaskID");
            AddForeignKey("dbo.Tasks", "ReoccurringParent_TaskID", "dbo.Tasks", "TaskID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ReoccurringParent_TaskID", "dbo.Tasks");
            DropIndex("dbo.Tasks", new[] { "ReoccurringParent_TaskID" });
            DropColumn("dbo.Tasks", "ReoccurringParent_TaskID");
            DropColumn("dbo.Tasks", "ReoccurringSchedule");
        }
    }
}
