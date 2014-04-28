namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoardFilter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "Filter", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boards", "Filter");
        }
    }
}
