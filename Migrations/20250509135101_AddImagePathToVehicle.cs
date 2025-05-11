using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftWheels.Migrations
{
    public partial class AddImagePathToVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Vehicles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Vehicles");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Vehicles",
                nullable: true);
        }
    }
}
