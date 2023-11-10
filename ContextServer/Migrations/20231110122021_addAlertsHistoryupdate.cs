using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context_aware_System.Migrations
{
    public partial class addAlertsHistoryupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "alertsHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "alertsHistories");
        }
    }
}
