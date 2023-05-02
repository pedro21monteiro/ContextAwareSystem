using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContinentalTestDb.Migrations
{
    public partial class setima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LineId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Device",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "Requests");
        }
    }
}
