namespace GradeBookApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        studentID = c.Int(nullable: false),
                        subjectID = c.Int(nullable: false),
                        Mark = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.studentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.subjectID, cascadeDelete: true)
                .Index(t => t.studentID)
                .Index(t => t.subjectID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        stream = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exams", "subjectID", "dbo.Subjects");
            DropForeignKey("dbo.Exams", "studentID", "dbo.Students");
            DropIndex("dbo.Exams", new[] { "subjectID" });
            DropIndex("dbo.Exams", new[] { "studentID" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Students");
            DropTable("dbo.Exams");
        }
    }
}
