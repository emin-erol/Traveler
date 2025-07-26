using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changedTheMileageToMileagePackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mileages");

            migrationBuilder.CreateTable(
                name: "MileagePackages",
                columns: table => new
                {
                    MileagePackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageLowLimit = table.Column<int>(type: "int", nullable: false),
                    PackageUpLimit = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileagePackages", x => x.MileagePackageId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MileagePackages");

            migrationBuilder.CreateTable(
                name: "Mileages",
                columns: table => new
                {
                    MileageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MileageType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mileages", x => x.MileageId);
                });
        }
    }
}
