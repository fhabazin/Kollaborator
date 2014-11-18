namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userGroupEdited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserGroupModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserGroupModels", "GroupModel_groupID", "dbo.GroupModels");
            DropIndex("dbo.ApplicationUserGroupModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserGroupModels", new[] { "GroupModel_groupID" });
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        groupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.groupID })
                .ForeignKey("dbo.GroupModels", t => t.groupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.groupID);
            
            AddColumn("dbo.GroupModels", "creator", c => c.String());
            DropColumn("dbo.GroupModels", "creatorID");
            DropTable("dbo.ApplicationUserGroupModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserGroupModels",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        GroupModel_groupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.GroupModel_groupID });
            
            AddColumn("dbo.GroupModels", "creatorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserGroups", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroups", "groupID", "dbo.GroupModels");
            DropIndex("dbo.UserGroups", new[] { "groupID" });
            DropIndex("dbo.UserGroups", new[] { "UserID" });
            DropColumn("dbo.GroupModels", "creator");
            DropTable("dbo.UserGroups");
            CreateIndex("dbo.ApplicationUserGroupModels", "GroupModel_groupID");
            CreateIndex("dbo.ApplicationUserGroupModels", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserGroupModels", "GroupModel_groupID", "dbo.GroupModels", "groupID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserGroupModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
