using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContinentalTestDb.Migrations
{
    public partial class quinta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Workers_workerId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "workerId",
                table: "Requests",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_workerId",
                table: "Requests",
                newName: "IX_Requests_WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Workers_WorkerId",
                table: "Requests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Workers_WorkerId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Requests",
                newName: "workerId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_WorkerId",
                table: "Requests",
                newName: "IX_Requests_workerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Workers_workerId",
                table: "Requests",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
