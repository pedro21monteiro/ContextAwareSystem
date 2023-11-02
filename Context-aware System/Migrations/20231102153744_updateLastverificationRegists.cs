using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context_aware_System.Migrations
{
    public partial class updateLastverificationRegists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentProductsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "ComponentsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "CoordinatorsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "DevicesVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "LinesVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "OperatorsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "ProductionPlansVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "ProductsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "ReasonsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "RequestsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "Schedule_worker_linesVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "SupervisorsVerification",
                table: "lastVerificationRegists");

            migrationBuilder.DropColumn(
                name: "WorkersVerification",
                table: "lastVerificationRegists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ComponentProductsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ComponentsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CoordinatorsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DevicesVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LinesVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OperatorsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionPlansVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReasonsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Schedule_worker_linesVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SupervisorsVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkersVerification",
                table: "lastVerificationRegists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
