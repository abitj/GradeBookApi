namespace GradeBookApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradeBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "Marks", c => c.Single(nullable: false));
            DropColumn("dbo.Exams", "Mark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exams", "Mark", c => c.Single(nullable: false));
            DropColumn("dbo.Exams", "Marks");
        }
    }
}
