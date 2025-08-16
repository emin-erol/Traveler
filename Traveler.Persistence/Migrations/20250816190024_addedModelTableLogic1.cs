using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedModelTableLogic1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarClasses_CarClassId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BigImageUrl",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Luggage",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Seat",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Cars",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "CarClassId",
                table: "Cars",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarClassId",
                table: "Cars",
                newName: "IX_Cars_ModelId");

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seat = table.Column<byte>(type: "tinyint", nullable: false),
                    Luggage = table.Column<byte>(type: "tinyint", nullable: false),
                    BigImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CarClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Model_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Model_CarClasses_CarClassId",
                        column: x => x.CarClassId,
                        principalTable: "CarClasses",
                        principalColumn: "CarClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model_BrandId",
                table: "Model",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Model_CarClassId",
                table: "Model",
                column: "CarClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Model_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Model_ModelId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Cars",
                newName: "CarClassId");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "Cars",
                newName: "Model");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                newName: "IX_Cars_CarClassId");

            migrationBuilder.AddColumn<string>(
                name: "BigImageUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Luggage",
                table: "Cars",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Seat",
                table: "Cars",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarClasses_CarClassId",
                table: "Cars",
                column: "CarClassId",
                principalTable: "CarClasses",
                principalColumn: "CarClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
