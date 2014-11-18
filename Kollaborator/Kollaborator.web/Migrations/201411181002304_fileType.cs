namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "FileTeype", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileModels", "FileTeype");
        }
    }
}
