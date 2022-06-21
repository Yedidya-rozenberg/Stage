using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class interest_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Users_StudentId",
                table: "Interest");

            migrationBuilder.DropIndex(
                name: "IX_Interest_StudentId",
                table: "Interest");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Interest");

            migrationBuilder.CreateTable(
                name: "CourseInterest",
                columns: table => new
                {
                    CourseTagsInterestId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoursesCourseID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInterest", x => new { x.CourseTagsInterestId, x.CoursesCourseID });
                    table.ForeignKey(
                        name: "FK_CourseInterest_Courses_CoursesCourseID",
                        column: x => x.CoursesCourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseInterest_Interest_CourseTagsInterestId",
                        column: x => x.CourseTagsInterestId,
                        principalTable: "Interest",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterestStudent",
                columns: table => new
                {
                    InterestsInterestId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestStudent", x => new { x.InterestsInterestId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_InterestStudent_Interest_InterestsInterestId",
                        column: x => x.InterestsInterestId,
                        principalTable: "Interest",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestStudent_Users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseInterest_CoursesCourseID",
                table: "CourseInterest",
                column: "CoursesCourseID");

            migrationBuilder.CreateIndex(
                name: "IX_InterestStudent_StudentsId",
                table: "InterestStudent",
                column: "StudentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInterest");

            migrationBuilder.DropTable(
                name: "InterestStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Interest",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interest_StudentId",
                table: "Interest",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Users_StudentId",
                table: "Interest",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
