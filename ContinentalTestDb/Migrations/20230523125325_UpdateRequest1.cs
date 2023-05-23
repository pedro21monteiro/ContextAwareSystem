using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContinentalTestDb.Migrations
{
    public partial class UpdateRequest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Workers_WorkerId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_WorkerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Device",
                table: "Requests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_WorkerId",
                table: "Requests",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Workers_WorkerId",
                table: "Requests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
