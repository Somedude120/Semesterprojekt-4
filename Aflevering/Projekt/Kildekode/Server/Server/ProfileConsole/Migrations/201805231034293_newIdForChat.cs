namespace ProfileConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newIdForChat : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Chat");
            //CreateTable(
            //    "dbo.__MigrationHistory",
            //    c => new
            //        {
            //            MigrationId = c.String(nullable: false, maxLength: 150),
            //            ContextKey = c.String(nullable: false, maxLength: 300),
            //            Model = c.Binary(nullable: false),
            //            ProductVersion = c.String(nullable: false, maxLength: 32),
            //        })
            //    .PrimaryKey(t => new { t.MigrationId, t.ContextKey });
            
            //AddColumn("dbo.Chat", "Id", c => c.Int(nullable: false));
            //AlterColumn("dbo.Chat", "Message", c => c.String(unicode: false));
            //AddPrimaryKey("dbo.Chat", "Id");
            //DropColumn("dbo.Chat", "MessageNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chat", "MessageNumber", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Chat");
            AlterColumn("dbo.Chat", "Message", c => c.String());
            DropColumn("dbo.Chat", "Id");
            DropTable("dbo.__MigrationHistory");
            AddPrimaryKey("dbo.Chat", new[] { "MessageNumber", "Sender", "Receiver" });
        }
    }
}
