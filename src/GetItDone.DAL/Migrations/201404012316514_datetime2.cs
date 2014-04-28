namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Joined", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tasks", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tasks", "Due", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "Due", c => c.DateTime());
            AlterColumn("dbo.Tasks", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "Joined");
        }
    }
}
