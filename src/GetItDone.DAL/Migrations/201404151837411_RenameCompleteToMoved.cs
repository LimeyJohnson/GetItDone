namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCompleteToMoved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Moved", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Tasks", "Completed");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "Completed", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Tasks", "Moved");
        }
    }
}
