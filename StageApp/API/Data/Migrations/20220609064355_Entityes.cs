using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Entityes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_AppUserId",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Payment",
                table: "Users",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Salary",
                table: "Users",
                type: "REAL",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Photos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Photos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseName = table.Column<string>(type: "TEXT", nullable: true),
                    CourseStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    TeacherID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Users_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_Interest_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesCourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesCourseID, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CoursesCourseID",
                        column: x => x.CoursesCourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Users_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitName = table.Column<string>(type: "TEXT", nullable: true),
                    StudyContent = table.Column<string>(type: "TEXT", nullable: true),
                    Questions = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_Units_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CourseID",
                table: "Photos",
                column: "CourseID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherID",
                table: "Courses",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsId",
                table: "CourseStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Interest_StudentId",
                table: "Interest",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseID",
                table: "Units",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Courses_CourseID",
                table: "Photos",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Courses_CourseID",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_AppUserId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CourseID",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Photos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
