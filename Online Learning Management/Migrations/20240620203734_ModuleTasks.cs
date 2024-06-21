using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class ModuleTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ModuleID",
                table: "ModuleTasks",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateCreated",
                table: "ModuleTasks",
                type: "date",
                nullable: true,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "ModuleTasks",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ModuleTasks",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Modules",
                type: "nvarchar(3000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseID",
                table: "Modules",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AssessmentMethods",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Modules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "Modules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "LearningOutcomes",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prerequisites",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resources",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Modules",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", precision: 5, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.CheckConstraint("CK_Grade_Score", "[Score] >= 0 AND [Score] <= 100");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleTasks_ModuleID",
                table: "ModuleTasks",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CourseID",
                table: "Modules",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseID",
                table: "Modules",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleTasks_Modules_ModuleID",
                table: "ModuleTasks",
                column: "ModuleID",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseID",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleTasks_Modules_ModuleID",
                table: "ModuleTasks");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_ModuleTasks_ModuleID",
                table: "ModuleTasks");

            migrationBuilder.DropIndex(
                name: "IX_Modules_CourseID",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ModuleTasks");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "ModuleTasks");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ModuleTasks");

            migrationBuilder.DropColumn(
                name: "AssessmentMethods",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "LearningOutcomes",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Prerequisites",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "Resources",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Modules");

            migrationBuilder.AlterColumn<string>(
                name: "ModuleID",
                table: "ModuleTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)");

            migrationBuilder.AlterColumn<string>(
                name: "CourseID",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
