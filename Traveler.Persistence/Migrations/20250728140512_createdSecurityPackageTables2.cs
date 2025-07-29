using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class createdSecurityPackageTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "packageOptions",
                columns: table => new
                {
                    PackageOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packageOptions", x => x.PackageOptionId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityPackages",
                columns: table => new
                {
                    SecurityPackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPackages", x => x.SecurityPackageId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityPackageOptions",
                columns: table => new
                {
                    SecurityPackageOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageOptionId = table.Column<int>(type: "int", nullable: false),
                    SecurityPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPackageOptions", x => x.SecurityPackageOptionId);
                    table.ForeignKey(
                        name: "FK_SecurityPackageOptions_SecurityPackages_SecurityPackageId",
                        column: x => x.SecurityPackageId,
                        principalTable: "SecurityPackages",
                        principalColumn: "SecurityPackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityPackageOptions_packageOptions_PackageOptionId",
                        column: x => x.PackageOptionId,
                        principalTable: "packageOptions",
                        principalColumn: "PackageOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPackageOptions_PackageOptionId",
                table: "SecurityPackageOptions",
                column: "PackageOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPackageOptions_SecurityPackageId",
                table: "SecurityPackageOptions",
                column: "SecurityPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityPackageOptions");

            migrationBuilder.DropTable(
                name: "SecurityPackages");

            migrationBuilder.DropTable(
                name: "packageOptions");
        }
    }
}
