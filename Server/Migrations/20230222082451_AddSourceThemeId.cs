using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class AddSourceThemeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceThemeId",
                table: "Themes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Themes_SourceThemeId",
                table: "Themes",
                column: "SourceThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Themes_SourceThemeId",
                table: "Themes",
                column: "SourceThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Themes_SourceThemeId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Themes_SourceThemeId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "SourceThemeId",
                table: "Themes");
        }
    }
}
