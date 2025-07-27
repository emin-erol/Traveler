using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatingLocationOprs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Locations",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartTime",
                table: "Locations",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_LocationAvailabilities_LocationId",
                table: "LocationAvailabilities",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationAvailabilities_Locations_LocationId",
                table: "LocationAvailabilities",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationAvailabilities_Locations_LocationId",
                table: "LocationAvailabilities");

            migrationBuilder.DropIndex(
                name: "IX_LocationAvailabilities_LocationId",
                table: "LocationAvailabilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Locations");
        }
    }
}
