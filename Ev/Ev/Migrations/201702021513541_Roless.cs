namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roless : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.RoleID })
                .ForeignKey("dbo.UserAccount", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserID", "dbo.UserAccount");
            DropIndex("dbo.UserRoles", new[] { "RoleID" });
            DropIndex("dbo.UserRoles", new[] { "UserID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
        }
    }
}
