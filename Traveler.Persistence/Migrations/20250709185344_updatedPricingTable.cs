using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedPricingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricingName",
                table: "Pricings",
                newName: "PricingType");

            migrationBuilder.AddColumn<decimal>(
                name: "PricingDec",
                table: "Pricings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricingDec",
                table: "Pricings");

            migrationBuilder.RenameColumn(
                name: "PricingType",
                table: "Pricings",
                newName: "PricingName");
        }
    }
}
