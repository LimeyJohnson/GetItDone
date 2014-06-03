namespace GetItDone.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayLists : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBoards", "BoardID", "dbo.Boards");
            DropForeignKey("dbo.UserBoards", "UserID", "dbo.Users");
            DropTable("dbo.UserBoards");

            CreateTable(
                "dbo.PlayLists",
                c => new
                    {
                        PlaylistID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        Member_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PlaylistID)
                .ForeignKey("dbo.Users", t => t.Member_UserID)
                .Index(t => t.Member_UserID);
            
            CreateTable(
                "dbo.PlayListTracks",
                c => new
                    {
                        Order = c.Int(nullable: false),
                        TrackID = c.Int(nullable: false),
                        PlayListID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order, t.TrackID, t.PlayListID })
                .ForeignKey("dbo.PlayLists", t => t.PlayListID, cascadeDelete: false)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: false)
                .Index(t => t.TrackID)
                .Index(t => t.PlayListID);
            
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
                .PrimaryKey(t => t.TrackId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserPlayLists",
                c => new
                    {
                        PlayListID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayListID, t.UserID })
                .ForeignKey("dbo.PlayLists", t => t.PlayListID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.PlayListID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserBoards",
                c => new
                    {
                        BoardID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BoardID, t.UserID })
                .ForeignKey("dbo.Boards", t => t.BoardID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false);
            Sql("INSERT INTO dbo.UserBoards(BoardID, UserID) SELECT BoardID, UserID FROM dbo.Boards"); 
            
        }
        
        public override void Down()
        {
            
            
            DropForeignKey("dbo.UserBoards", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserBoards", "BoardID", "dbo.Boards");
            DropForeignKey("dbo.Tracks", "UserID", "dbo.Users");
            DropForeignKey("dbo.PlayListTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.PlayListTracks", "PlayListID", "dbo.PlayLists");
            DropForeignKey("dbo.UserPlayLists", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserPlayLists", "PlayListID", "dbo.PlayLists");
            DropForeignKey("dbo.PlayLists", "Member_UserID", "dbo.Users");
            DropIndex("dbo.UserPlayLists", new[] { "UserID" });
            DropIndex("dbo.UserPlayLists", new[] { "PlayListID" });
            DropIndex("dbo.Tracks", new[] { "UserID" });
            DropIndex("dbo.PlayListTracks", new[] { "PlayListID" });
            DropIndex("dbo.PlayListTracks", new[] { "TrackID" });
            DropIndex("dbo.PlayLists", new[] { "Member_UserID" });
            DropTable("dbo.UserBoards");
            DropTable("dbo.UserPlayLists");
            DropTable("dbo.Tracks");
            DropTable("dbo.PlayListTracks");
            DropTable("dbo.PlayLists");

            CreateTable(
                "dbo.UserBoards",
                c => new
                {
                    UserID = c.Int(nullable: false),
                    BoardID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserID, t.BoardID });
            AddForeignKey("dbo.UserBoards", "UserID", "dbo.Users", "UserID", cascadeDelete: false);
            AddForeignKey("dbo.UserBoards", "BoardID", "dbo.Boards", "BoardID", cascadeDelete: false);
        }
    }
}
