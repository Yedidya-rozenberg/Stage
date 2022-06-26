using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RequestManager_Skip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    ManagerId = table.Column<int>(type: "INTEGER", nullable: true),
                    RequestDetails = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestStatus = table.Column<string>(type: "TEXT", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StudentID = table.Column<int>(type: "INTEGER", nullable: true),
                    TeacherID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_requests_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_requests_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requests_Users_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requests_Users_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_requests_CourseID",
                table: "requests",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_requests_ManagerId",
                table: "requests",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_StudentID",
                table: "requests",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_requests_TeacherID",
                table: "requests",
                column: "TeacherID");
        }
    }
}
