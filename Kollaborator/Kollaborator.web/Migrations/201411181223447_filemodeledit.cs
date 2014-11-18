namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filemodeledit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "FileType", c => c.String());
            DropColumn("dbo.FileModels", "FileTeype");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileModels", "FileTeype", c => c.String());
            DropColumn("dbo.FileModels", "FileType");
        }
    }
}
