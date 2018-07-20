﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTicketsSystem.Data.Migrations
{
    public partial class AddStatusToTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Trips",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trips");
        }
    }
}
