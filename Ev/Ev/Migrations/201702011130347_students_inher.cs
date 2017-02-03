namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class students_inher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "VirtualClassID", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "VirtualClass_VirtualClassID", c => c.Int());
            AddColumn("dbo.VirtualClass", "Student_StudentID", c => c.Int());
            CreateIndex("dbo.Student", "VirtualClass_VirtualClassID");
            CreateIndex("dbo.VirtualClass", "Student_StudentID");
            AddForeignKey("dbo.VirtualClass", "Student_StudentID", "dbo.Student", "StudentID");
            AddForeignKey("dbo.Student", "VirtualClass_VirtualClassID", "dbo.VirtualClass", "VirtualClassID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "VirtualClass_VirtualClassID", "dbo.VirtualClass");
            DropForeignKey("dbo.VirtualClass", "Student_StudentID", "dbo.Student");
            DropIndex("dbo.VirtualClass", new[] { "Student_StudentID" });
            DropIndex("dbo.Student", new[] { "VirtualClass_VirtualClassID" });
            DropColumn("dbo.VirtualClass", "Student_StudentID");
            DropColumn("dbo.Student", "VirtualClass_VirtualClassID");
            DropColumn("dbo.Student", "VirtualClassID");
        }
    }
}
