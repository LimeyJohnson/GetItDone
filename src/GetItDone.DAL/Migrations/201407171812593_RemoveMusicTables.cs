namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMusicTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayLists", "Member_UserID", "dbo.Users");
            DropForeignKey("dbo.UserPlayLists", "PlayListID", "dbo.PlayLists");
            DropForeignKey("dbo.UserPlayLists", "UserID", "dbo.Users");
            DropForeignKey("dbo.PlayListTracks", "PlayListID", "dbo.PlayLists");
            DropForeignKey("dbo.PlayListTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "UserID", "dbo.Users");
            DropForeignKey("dbo.Boards", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users");
            DropIndex("dbo.Boards", new[] { "UserID" });
            DropIndex("dbo.PlayLists", new[] { "Member_UserID" });
            DropIndex("dbo.PlayListTracks", new[] { "TrackID" });
            DropIndex("dbo.PlayListTracks", new[] { "PlayListID" });
            DropIndex("dbo.Tracks", new[] { "UserID" });
            DropIndex("dbo.Tasks", new[] { "Creator_UserID" });
            DropIndex("dbo.UserPlayLists", new[] { "PlayListID" });
            DropIndex("dbo.UserPlayLists", new[] { "UserID" });
            AddColumn("dbo.Boards", "Creator_UserID", c => c.Int());
            AlterColumn("dbo.Tasks", "Due", c => c.DateTime());
            AlterColumn("dbo.Tasks", "Moved", c => c.DateTime());
            AlterColumn("dbo.Tasks", "Creator_UserID", c => c.Int());
            CreateIndex("dbo.Boards", "Creator_UserID");
            CreateIndex("dbo.Tasks", "Creator_UserID");
            AddForeignKey("dbo.Boards", "Creator_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users", "UserID");
            DropTable("dbo.PlayLists");
            DropTable("dbo.PlayListTracks");
            DropTable("dbo.Tracks");
            DropTable("dbo.UserPlayLists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPlayLists",
                c => new
                    {
                        PlayListID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayListID, t.UserID });
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        SongName = c.String(),
                        Artist = c.String(),
                        Album = c.String(),
                        UserID = c.Int(nullable: false),
                        BlobURL = c.String(),
                    })
                .PrimaryKey(t => t.TrackId);
            
            CreateTable(
                "dbo.PlayListTracks",
                c => new
                    {
                        Order = c.Int(nullable: false),
                        TrackID = c.Int(nullable: false),
                        PlayListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order, t.TrackID, t.PlayListID });
            
            CreateTable(
                "dbo.PlayLists",
                c => new
                    {
                        PlaylistID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Member_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PlaylistID);
            
            DropForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users");
            DropForeignKey("dbo.Boards", "Creator_UserID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Creator_UserID" });
            DropIndex("dbo.Boards", new[] { "Creator_UserID" });
            AlterColumn("dbo.Tasks", "Creator_UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Tasks", "Moved", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tasks", "Due", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Boards", "Creator_UserID");
            CreateIndex("dbo.UserPlayLists", "UserID");
            CreateIndex("dbo.UserPlayLists", "PlayListID");
            CreateIndex("dbo.Tasks", "Creator_UserID");
            CreateIndex("dbo.Tracks", "UserID");
            CreateIndex("dbo.PlayListTracks", "PlayListID");
            CreateIndex("dbo.PlayListTracks", "TrackID");
            CreateIndex("dbo.PlayLists", "Member_UserID");
            CreateIndex("dbo.Boards", "UserID");
            AddForeignKey("dbo.Tasks", "Creator_UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Boards", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Tracks", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.PlayListTracks", "TrackID", "dbo.Tracks", "TrackId", cascadeDelete: true);
            AddForeignKey("dbo.PlayListTracks", "PlayListID", "dbo.PlayLists", "PlaylistID", cascadeDelete: true);
            AddForeignKey("dbo.UserPlayLists", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserPlayLists", "PlayListID", "dbo.PlayLists", "PlaylistID", cascadeDelete: true);
            AddForeignKey("dbo.PlayLists", "Member_UserID", "dbo.Users", "UserID");
        }
    }
}
