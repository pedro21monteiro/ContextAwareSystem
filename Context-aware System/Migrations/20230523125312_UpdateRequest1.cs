using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context_aware_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRequest1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_requests_Workers_WorkerId",
                table: "requests");

            migrationBuilder.DropIndex(
                name: "IX_requests_WorkerId",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "Device",
                table: "requests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_requests_WorkerId",
                table: "requests",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_requests_Workers_WorkerId",
                table: "requests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
