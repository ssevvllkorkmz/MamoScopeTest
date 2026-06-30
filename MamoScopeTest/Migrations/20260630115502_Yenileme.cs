using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamoScopeTest.Migrations
{
    /// <inheritdoc />
    public partial class Yenileme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "MotorDriver",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SerialNumber",
                table: "MotorDriver",
                type: "string",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
