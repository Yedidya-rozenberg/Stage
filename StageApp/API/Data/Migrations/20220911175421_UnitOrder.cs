using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UnitOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_BackUnitId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_NextUnitId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_BackUnitId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_NextUnitId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "BackUnitId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "NextUnitId",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "NodeId",
                table: "Units",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LinkedEntityNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NextId = table.Column<int>(type: "INTEGER", nullable: true),
                    PreviousId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedEntityNodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_NodeId",
                table: "Units",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_LinkedEntityNodes_NodeId",
                table: "Units",
                column: "NodeId",
                principalTable: "LinkedEntityNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_LinkedEntityNodes_NodeId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "LinkedEntityNodes");

            migrationBuilder.DropIndex(
                name: "IX_Units_NodeId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "BackUnitId",
                table: "Units",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NextUnitId",
                table: "Units",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_BackUnitId",
                table: "Units",
                column: "BackUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_NextUnitId",
                table: "Units",
                column: "NextUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_BackUnitId",
                table: "Units",
                column: "BackUnitId",
                principalTable: "Units",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_NextUnitId",
                table: "Units",
                column: "NextUnitId",
                principalTable: "Units",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
