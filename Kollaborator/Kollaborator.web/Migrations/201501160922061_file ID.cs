namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "fileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileModels", "fileId");
        }
    }
}
