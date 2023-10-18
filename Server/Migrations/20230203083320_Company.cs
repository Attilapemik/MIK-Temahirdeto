using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExternalCompanyId",
                table: "Themes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalCompanyName",
                table: "Themes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Themes_ExternalCompanyId",
                table: "Themes",
                column: "ExternalCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Companies_ExternalCompanyId",
                table: "Themes",
                column: "ExternalCompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Companies_ExternalCompanyId",
                table: "Themes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Themes_ExternalCompanyId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "ExternalCompanyId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "ExternalCompanyName",
                table: "Themes");
        }
    }
}
