namespace LabBigSchool_DoVanSang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        CouresID = c.Int(nullable: false),
                        AttendeeID = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CouresID, t.AttendeeID })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.AttendeeID)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "AttendeeID", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "Course_Id" });
            DropIndex("dbo.Attendances", new[] { "AttendeeID" });
            DropTable("dbo.Attendances");
        }
    }
}
