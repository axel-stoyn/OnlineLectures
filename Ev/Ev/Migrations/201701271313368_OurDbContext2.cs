namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurDbContext2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "DateStartOfTraining", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainings", "DateStartOfTraining");
        }
    }
}
