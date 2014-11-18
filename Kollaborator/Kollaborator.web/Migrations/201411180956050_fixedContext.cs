namespace Kollaborator.web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedContext : DbMigration
    {
        public override void Up()
        {

            
            DropTable("dbo.GroupModels");
            DropTable("dbo.FileModels");
            CreateTable(
                "dbo.FileModels",
                c => new
                    {
                        path = c.String(nullable: false, maxLength: 128),
                        groupId = c.Int(nullable: false),
                        uploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.path);
            
            CreateTable(
                "dbo.GroupModels",
                c => new
                    {
                        groupID = c.Int(nullable: false, identity: true),
                        groupName = c.String(),
                        creatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.groupID);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ApplicationUserGroupModels", "GroupModel_groupID", "dbo.GroupModels");
            DropForeignKey("dbo.ApplicationUserGroupModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserGroupModels", new[] { "GroupModel_groupID" });
            DropIndex("dbo.ApplicationUserGroupModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.ApplicationUserGroupModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.GroupModels");
            DropTable("dbo.FileModels");
        }
    }
}
