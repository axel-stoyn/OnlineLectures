namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OurDbContext1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingID = c.Int(nullable: false, identity: true),
                        NameTraining = c.String(),
                    })
                .PrimaryKey(t => t.TrainingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trainings");
        }
    }
}
