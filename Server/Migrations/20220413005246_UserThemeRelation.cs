using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class UserThemeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Users_InstructorId",
                table: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Users_InstructorId",
                table: "Themes",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Users_InstructorId",
                table: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Users_InstructorId",
                table: "Themes",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
