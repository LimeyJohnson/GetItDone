namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecurringTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.TaskSchedules", "InitialBoard_BoardID", c => c.Int());
            AddColumn("dbo.TaskSchedules", "RecurringTask_TaskID", c => c.Int());
            CreateIndex("dbo.TaskSchedules", "InitialBoard_BoardID");
            CreateIndex("dbo.TaskSchedules", "RecurringTask_TaskID");
            AddForeignKey("dbo.TaskSchedules", "InitialBoard_BoardID", "dbo.Boards", "BoardID");
            AddForeignKey("dbo.TaskSchedules", "RecurringTask_TaskID", "dbo.Tasks", "TaskID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskSchedules", "RecurringTask_TaskID", "dbo.Tasks");
            DropForeignKey("dbo.TaskSchedules", "InitialBoard_BoardID", "dbo.Boards");
            DropIndex("dbo.TaskSchedules", new[] { "RecurringTask_TaskID" });
            DropIndex("dbo.TaskSchedules", new[] { "InitialBoard_BoardID" });
            DropColumn("dbo.TaskSchedules", "RecurringTask_TaskID");
            DropColumn("dbo.TaskSchedules", "InitialBoard_BoardID");
            DropColumn("dbo.Tasks", "Discriminator");
        }
    }
}
