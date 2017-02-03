namespace Ev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexVC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VirtualClass", "TrainingID", c => c.Int(nullable: false));
            AddColumn("dbo.VirtualClass", "Training_TrainingID", c => c.Int());
            CreateIndex("dbo.VirtualClass", "Training_TrainingID");
            AddForeignKey("dbo.VirtualClass", "Training_TrainingID", "dbo.Training", "TrainingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VirtualClass", "Training_TrainingID", "dbo.Training");
            DropIndex("dbo.VirtualClass", new[] { "Training_TrainingID" });
            DropColumn("dbo.VirtualClass", "Training_TrainingID");
            DropColumn("dbo.VirtualClass", "TrainingID");
        }
    }
}
