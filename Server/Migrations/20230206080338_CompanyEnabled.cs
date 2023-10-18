using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class CompanyEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalCompanyName",
                table: "Themes");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "ExternalCompanyName",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
