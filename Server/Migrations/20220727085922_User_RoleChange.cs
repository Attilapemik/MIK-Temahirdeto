using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PannonBlazor.Server.Migrations
{
    public partial class User_RoleChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Insert into RoleUser (UsersId, RolesId)
                                    SELECT u.Id as UsersId
	                                    ,r.Id as RolesId
                                    FROM Users u
                                    join Roles r on r.Name = u.Role");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
