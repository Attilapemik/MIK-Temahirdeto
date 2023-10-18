using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class Theme_IsExternal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "Themes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "Themes");
        }
    }
}
