namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Enroll2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Student", "VirtualClass_VirtualClassID", "dbo.VirtualClass");
            DropIndex("dbo.Student", new[] { "VirtualClass_VirtualClassID" });
            CreateTable(
                "dbo.Enrollmentss",
                c => new
                    {
                        VirtualClassID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VirtualClassID, t.StudentID })
                .ForeignKey("dbo.VirtualClass", t => t.VirtualClassID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.VirtualClassID)
                .Index(t => t.StudentID);
            
            DropColumn("dbo.Student", "VirtualClass_VirtualClassID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Student", "VirtualClass_VirtualClassID", c => c.Int());
            DropForeignKey("dbo.Enrollmentss", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Enrollmentss", "VirtualClassID", "dbo.VirtualClass");
            DropIndex("dbo.Enrollmentss", new[] { "StudentID" });
            DropIndex("dbo.Enrollmentss", new[] { "VirtualClassID" });
            DropTable("dbo.Enrollmentss");
            CreateIndex("dbo.Student", "VirtualClass_VirtualClassID");
            AddForeignKey("dbo.Student", "VirtualClass_VirtualClassID", "dbo.VirtualClass", "VirtualClassID");
        }
    }
}
