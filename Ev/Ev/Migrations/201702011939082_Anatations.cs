namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Anatations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.VirtualClass", "NameVirtualClass", c => c.String(nullable: false));
            AlterColumn("dbo.Training", "NameTraining", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Training", "NameTraining", c => c.String());
            AlterColumn("dbo.VirtualClass", "NameVirtualClass", c => c.String());
            AlterColumn("dbo.Student", "LastName", c => c.String());
            AlterColumn("dbo.Student", "FirstName", c => c.String());
        }
    }
}
