namespace ProfileConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dexcriptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformation", "Description", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInformation", "Description");
        }
    }
}
