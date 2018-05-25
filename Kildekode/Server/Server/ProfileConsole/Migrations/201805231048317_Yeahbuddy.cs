namespace ProfileConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yeahbuddy : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Chat");
            AlterColumn("dbo.Chat", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Chat", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Chat");
            AlterColumn("dbo.Chat", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Chat", "Id");
        }
    }
}
