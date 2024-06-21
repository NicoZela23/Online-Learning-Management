using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "DurationInWeeks",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DurationInWeeks",
                table: "Courses");
        }
    }
}
