using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context_aware_System.Migrations
{
    public partial class updateLastverificationRegistsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.RenameColumn(
                name: "StopsVerification",
                table: "lastVerificationRegists",
                newName: "LastVerification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastVerification",
                table: "lastVerificationRegists",
                newName: "StopsVerification");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
