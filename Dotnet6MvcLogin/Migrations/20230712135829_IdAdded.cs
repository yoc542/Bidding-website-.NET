using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet6MvcLogin.Migrations
{
    public partial class IdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ApplicationForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationForms");
        }
    }
}
