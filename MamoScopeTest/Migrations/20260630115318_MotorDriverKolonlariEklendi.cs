using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamoScopeTest.Migrations
{
    /// <inheritdoc />
    public partial class MotorDriverKolonlariEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "MotorDriver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "MotorDriver",
                type: "string",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestDate",
                table: "MotorDriver",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Voltage",
                table: "MotorDriver",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "MotorDriver");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "MotorDriver");

            migrationBuilder.DropColumn(
                name: "TestDate",
                table: "MotorDriver");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "MotorDriver");
        }
    }
}
