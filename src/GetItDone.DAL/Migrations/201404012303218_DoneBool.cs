namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoneBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Done", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Done");
        }
    }
}
