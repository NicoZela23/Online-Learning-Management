using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseStudentRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseStudents_CourseID",
                table: "CourseStudents",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudents_StudentID",
                table: "CourseStudents",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_Courses_CourseID",
                table: "CourseStudents",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudents_Students_StudentID",
                table: "CourseStudents",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_Courses_CourseID",
                table: "CourseStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudents_Students_StudentID",
                table: "CourseStudents");

            migrationBuilder.DropIndex(
                name: "IX_CourseStudents_CourseID",
                table: "CourseStudents");

            migrationBuilder.DropIndex(
                name: "IX_CourseStudents_StudentID",
                table: "CourseStudents");
        }
    }
}
