using Microsoft.EntityFrameworkCore.Migrations;

namespace Stations.Data.Migrations
{
    public partial class EnumsMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Trips",
                maxLength: 7,
                nullable: false,
                defaultValue: "OnTime",
                oldClrType: typeof(string),
                oldDefaultValue: "OnTime");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Trains",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "CustomerCards",
                maxLength: 11,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldDefaultValue: "Normal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Trips",
                nullable: false,
                defaultValue: "OnTime",
                oldClrType: typeof(string),
                oldMaxLength: 7,
                oldDefaultValue: "OnTime");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Trains",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "CustomerCards",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldDefaultValue: "Normal");
        }
    }
}
