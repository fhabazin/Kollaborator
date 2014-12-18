namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatModels", "sender", c => c.String());
            AddColumn("dbo.ChatModels", "groupID", c => c.Int(nullable: false));
            DropColumn("dbo.ChatModels", "username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChatModels", "username", c => c.String());
            DropColumn("dbo.ChatModels", "groupID");
            DropColumn("dbo.ChatModels", "sender");
        }
    }
}
