using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class ReportCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

            

            migrationBuilder.CreateTable(
                name: "ReportCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProgressPercentage = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCourses", x => x.Id);
                });

                  

            

            

            

            migrationBuilder.CreateIndex(
                name: "IX_ReportCourses_StudentID_CourseID",
                table: "ReportCourses",
                columns: new[] { "StudentID", "CourseID" },
                unique: true);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "ReportCourses");

            
        }
    }
}
