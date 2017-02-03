namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurDbContext3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VirtualClasses",
                c => new
                    {
                        VirtualClassID = c.Int(nullable: false, identity: true),
                        NameVirtualClass = c.String(),
                    })
                .PrimaryKey(t => t.VirtualClassID);
            
            AddColumn("dbo.Students", "FirstName", c => c.String());
            DropColumn("dbo.Students", "FirstMidName");
            DropColumn("dbo.Students", "EnrollmentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "EnrollmentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Students", "FirstMidName", c => c.String());
            DropColumn("dbo.Students", "FirstName");
            DropTable("dbo.VirtualClasses");
        }
    }
}
