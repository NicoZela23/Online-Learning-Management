using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class CourseStudenstMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.DropTable(
                name: "CourseStudents");

            // migrationBuilder.DropTable(
            //     name: "ModuleTasks");
        }
    }
}
