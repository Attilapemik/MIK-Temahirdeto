using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class AddStudentProgrammeToTheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentProgrammeId",
                table: "Themes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Themes_StudentProgrammeId",
                table: "Themes",
                column: "StudentProgrammeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Programmes_StudentProgrammeId",
                table: "Themes",
                column: "StudentProgrammeId",
                principalTable: "Programmes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Programmes_StudentProgrammeId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Themes_StudentProgrammeId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "StudentProgrammeId",
                table: "Themes");
        }
    }
}
