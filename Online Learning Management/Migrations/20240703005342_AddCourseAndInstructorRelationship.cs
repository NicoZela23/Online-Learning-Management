using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseAndInstructorRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdInstructor",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IdInstructor",
                table: "Courses",
                column: "IdInstructor");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_IdInstructor",
                table: "Courses",
                column: "IdInstructor",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instructors_IdInstructor",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_IdInstructor",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IdInstructor",
                table: "Courses");
        }
    }
}
