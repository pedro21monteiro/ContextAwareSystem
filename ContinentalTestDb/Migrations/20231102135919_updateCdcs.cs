using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContinentalTestDb.Migrations
{
    public partial class updateCdcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStop",
                table: "cdc_Stops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProduction",
                table: "cdc_Productions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdStop",
                table: "cdc_Stops");

            migrationBuilder.DropColumn(
                name: "IdProduction",
                table: "cdc_Productions");
        }
    }
}
