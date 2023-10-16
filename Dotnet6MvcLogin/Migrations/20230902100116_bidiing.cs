using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet6MvcLogin.Migrations
{
    public partial class bidiing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ApplicationForms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "ApplicationForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "productPrice",
                table: "ApplicationForms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ApplicationForms");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "ApplicationForms");

            migrationBuilder.DropColumn(
                name: "productPrice",
                table: "ApplicationForms");
        }
    }
}
