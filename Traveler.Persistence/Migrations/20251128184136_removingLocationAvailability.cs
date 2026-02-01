using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removingLocationAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationAvailabilities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationAvailabilities",
                columns: table => new
                {
                    LocationAvailabilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAvailabilities", x => x.LocationAvailabilityId);
                    table.ForeignKey(
                        name: "FK_LocationAvailabilities_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationAvailabilities_LocationId",
                table: "LocationAvailabilities",
                column: "LocationId");
        }
    }
}
