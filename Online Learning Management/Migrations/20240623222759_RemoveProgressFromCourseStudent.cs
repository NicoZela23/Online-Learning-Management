using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProgressFromCourseStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Eliminar el campo Progress de la tabla CourseStudents
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "CourseStudents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Volver a agregar el campo Progress en caso de que se deshaga la migración
            migrationBuilder.AddColumn<decimal>(
                name: "Progress",
                table: "CourseStudents",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }

}
