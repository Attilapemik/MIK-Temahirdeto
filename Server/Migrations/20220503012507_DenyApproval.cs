using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class DenyApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeProgrammes_Semesters_SemesterId",
                table: "ThemeProgrammes");

            migrationBuilder.InsertData(
                table: "Approvals",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Elutasítva" });

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeProgrammes_Semesters_SemesterId",
                table: "ThemeProgrammes",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeProgrammes_Semesters_SemesterId",
                table: "ThemeProgrammes");

            migrationBuilder.DeleteData(
                table: "Approvals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeProgrammes_Semesters_SemesterId",
                table: "ThemeProgrammes",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
