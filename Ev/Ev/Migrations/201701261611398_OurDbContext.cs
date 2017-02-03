namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
        }
    }
}
