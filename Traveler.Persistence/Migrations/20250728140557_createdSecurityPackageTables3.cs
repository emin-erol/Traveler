using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class createdSecurityPackageTables3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecurityPackageOptions_packageOptions_PackageOptionId",
                table: "SecurityPackageOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_packageOptions",
                table: "packageOptions");

            migrationBuilder.RenameTable(
                name: "packageOptions",
                newName: "PackageOptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageOptions",
                table: "PackageOptions",
                column: "PackageOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityPackageOptions_PackageOptions_PackageOptionId",
                table: "SecurityPackageOptions",
                column: "PackageOptionId",
                principalTable: "PackageOptions",
                principalColumn: "PackageOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecurityPackageOptions_PackageOptions_PackageOptionId",
                table: "SecurityPackageOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageOptions",
                table: "PackageOptions");

            migrationBuilder.RenameTable(
                name: "PackageOptions",
                newName: "packageOptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_packageOptions",
                table: "packageOptions",
                column: "PackageOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityPackageOptions_packageOptions_PackageOptionId",
                table: "SecurityPackageOptions",
                column: "PackageOptionId",
                principalTable: "packageOptions",
                principalColumn: "PackageOptionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
