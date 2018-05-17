namespace ProfileConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        MessageNumber = c.Int(nullable: false),
                        Sender = c.String(nullable: false, maxLength: 15, unicode: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => new { t.GroupId, t.MessageNumber, t.Sender })
                .ForeignKey("dbo.ChatGroups", t => t.GroupId)
                .ForeignKey("dbo.UserInformation", t => t.Sender)
                .Index(t => t.GroupId)
                .Index(t => t.Sender);
            
            CreateTable(
                "dbo.ChatGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.UserInformation",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 15, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Status = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.FriendList",
                c => new
                    {
                        User1 = c.String(nullable: false, maxLength: 15, unicode: false),
                        User2 = c.String(nullable: false, maxLength: 15, unicode: false),
                        Status = c.String(maxLength: 10, fixedLength: true),
                        Action_User = c.String(nullable: false, maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => new { t.User1, t.User2 })
                .ForeignKey("dbo.UserInformation", t => t.Action_User)
                .ForeignKey("dbo.UserInformation", t => t.User1)
                .ForeignKey("dbo.UserInformation", t => t.User2)
                .Index(t => t.User1)
                .Index(t => t.User2)
                .Index(t => t.Action_User);
            
            CreateTable(
                "dbo.Login",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 15, unicode: false),
                        Salt = c.String(unicode: false),
                        Hash = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.UserInformation", t => t.Username)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TagName);
            
            CreateTable(
                "dbo.Emoji",
                c => new
                    {
                        EmojiShortcut = c.String(nullable: false, maxLength: 50, unicode: false),
                        Emoji = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => new { t.EmojiShortcut, t.Emoji });
            
            CreateTable(
                "dbo.UserTags",
                c => new
                    {
                        TagName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => new { t.TagName, t.UserName })
                .ForeignKey("dbo.Tags", t => t.TagName, cascadeDelete: true)
                .ForeignKey("dbo.UserInformation", t => t.UserName, cascadeDelete: true)
                .Index(t => t.TagName)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.UserChatGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.UserName })
                .ForeignKey("dbo.ChatGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserInformation", t => t.UserName, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserChatGroups", "UserName", "dbo.UserInformation");
            DropForeignKey("dbo.UserChatGroups", "GroupId", "dbo.ChatGroups");
            DropForeignKey("dbo.UserTags", "UserName", "dbo.UserInformation");
            DropForeignKey("dbo.UserTags", "TagName", "dbo.Tags");
            DropForeignKey("dbo.Login", "Username", "dbo.UserInformation");
            DropForeignKey("dbo.FriendList", "User2", "dbo.UserInformation");
            DropForeignKey("dbo.FriendList", "User1", "dbo.UserInformation");
            DropForeignKey("dbo.FriendList", "Action_User", "dbo.UserInformation");
            DropForeignKey("dbo.Chat", "Sender", "dbo.UserInformation");
            DropForeignKey("dbo.Chat", "GroupId", "dbo.ChatGroups");
            DropIndex("dbo.UserChatGroups", new[] { "UserName" });
            DropIndex("dbo.UserChatGroups", new[] { "GroupId" });
            DropIndex("dbo.UserTags", new[] { "UserName" });
            DropIndex("dbo.UserTags", new[] { "TagName" });
            DropIndex("dbo.Login", new[] { "Username" });
            DropIndex("dbo.FriendList", new[] { "Action_User" });
            DropIndex("dbo.FriendList", new[] { "User2" });
            DropIndex("dbo.FriendList", new[] { "User1" });
            DropIndex("dbo.Chat", new[] { "Sender" });
            DropIndex("dbo.Chat", new[] { "GroupId" });
            DropTable("dbo.UserChatGroups");
            DropTable("dbo.UserTags");
            DropTable("dbo.Emoji");
            DropTable("dbo.Tags");
            DropTable("dbo.Login");
            DropTable("dbo.FriendList");
            DropTable("dbo.UserInformation");
            DropTable("dbo.ChatGroups");
            DropTable("dbo.Chat");
        }
    }
}