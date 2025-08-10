using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addLocationIdColumnToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LocationId",
                table: "Cars",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Locations_LocationId",
                table: "Cars",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Locations_LocationId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_LocationId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Cars");
        }
    }
}
