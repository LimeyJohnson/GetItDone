namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskSchedule : DbMigration
    {
        public override void Up()
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
            CreateIndex("dbo.Tasks", "Schedule_TaskScheduleID");
            AddForeignKey("dbo.Tasks", "Schedule_TaskScheduleID", "dbo.TaskSchedules", "TaskScheduleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Schedule_TaskScheduleID", "dbo.TaskSchedules");
            DropIndex("dbo.Tasks", new[] { "Schedule_TaskScheduleID" });
            DropColumn("dbo.Tasks", "Schedule_TaskScheduleID");
            DropTable("dbo.TaskSchedules");
        }
    }
}
