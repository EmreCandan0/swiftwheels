using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftWheels.Migrations
{
    public partial class UpdateRentedVehicleWithDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "RentedVehicles");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RentedVehicles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "RentedVehicles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "RentedVehicles",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RentedVehicles");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "RentedVehicles");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "RentedVehicles");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "RentedVehicles",
                nullable: true);
        }
    }
}
