using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Learning_Management.Migrations
{
    /// <inheritdoc />
    public partial class TaskStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleTaskID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Comment = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStudents_ModuleTasks_ModuleTaskID",
                        column: x => x.ModuleTaskID,
                        principalTable: "ModuleTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskStudents_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskStudents_UploadedFiles_FileID",
                        column: x => x.FileID,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskStudents_FileID",
                table: "TaskStudents",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStudents_ModuleTaskID",
                table: "TaskStudents",
                column: "ModuleTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStudents_StudentID",
                table: "TaskStudents",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskStudents");
        }
    }
}
