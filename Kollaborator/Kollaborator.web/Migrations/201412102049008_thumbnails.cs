namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thumbnails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileModels", "thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FileModels", "thumbnail");
        }
    }
}
