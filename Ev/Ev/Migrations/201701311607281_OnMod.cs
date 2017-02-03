namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnMod : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Students", newName: "Student");
            RenameTable(name: "dbo.Trainings", newName: "Training");
            RenameTable(name: "dbo.UserAccounts", newName: "UserAccount");
            RenameTable(name: "dbo.VirtualClasses", newName: "VirtualClass");
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        TrainingID = c.Int(nullable: false),
                        VirtualClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainingID, t.VirtualClassID })
                .ForeignKey("dbo.Training", t => t.TrainingID, cascadeDelete: true)
                .ForeignKey("dbo.VirtualClass", t => t.VirtualClassID, cascadeDelete: true)
                .Index(t => t.TrainingID)
                .Index(t => t.VirtualClassID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "VirtualClassID", "dbo.VirtualClass");
            DropForeignKey("dbo.Enrollment", "TrainingID", "dbo.Training");
            DropIndex("dbo.Enrollment", new[] { "VirtualClassID" });
            DropIndex("dbo.Enrollment", new[] { "TrainingID" });
            DropTable("dbo.Enrollment");
            RenameTable(name: "dbo.VirtualClass", newName: "VirtualClasses");
            RenameTable(name: "dbo.UserAccount", newName: "UserAccounts");
            RenameTable(name: "dbo.Training", newName: "Trainings");
            RenameTable(name: "dbo.Student", newName: "Students");
        }
    }
}
