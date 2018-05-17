namespace ProfileConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedName : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.UserInformation", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInformation", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
