namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usergroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                  "dbo.ApplicationUserGroupModels",
                  c => new
                  {
                      ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                      GroupModel_groupID = c.Int(nullable: false),
                  })
                  .PrimaryKey(t => new { t.ApplicationUser_Id, t.GroupModel_groupID })
                  .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                  .ForeignKey("dbo.GroupModels", t => t.GroupModel_groupID, cascadeDelete: true)
                  .Index(t => t.ApplicationUser_Id)
                  .Index(t => t.GroupModel_groupID);
        }
        
        public override void Down()
        {
            DropTable(
               "dbo.ApplicationUserGroupModels"
              );
        }
    }
}
