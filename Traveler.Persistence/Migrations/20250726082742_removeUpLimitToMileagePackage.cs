using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeUpLimitToMileagePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageLowLimit",
                table: "MileagePackages");

            migrationBuilder.RenameColumn(
                name: "PackageUpLimit",
                table: "MileagePackages",
                newName: "PackageLimit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PackageLimit",
                table: "MileagePackages",
                newName: "PackageUpLimit");

            migrationBuilder.AddColumn<int>(
                name: "PackageLowLimit",
                table: "MileagePackages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
