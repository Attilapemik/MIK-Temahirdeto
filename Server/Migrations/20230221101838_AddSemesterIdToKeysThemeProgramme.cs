using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class AddSemesterIdToKeysThemeProgramme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThemeProgrammes",
                table: "ThemeProgrammes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThemeProgrammes",
                table: "ThemeProgrammes",
                columns: new[] { "ThemeId", "ProgrammeId", "SemesterId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ThemeProgrammes",
                table: "ThemeProgrammes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThemeProgrammes",
                table: "ThemeProgrammes",
                columns: new[] { "ThemeId", "ProgrammeId" });
        }
    }
}
