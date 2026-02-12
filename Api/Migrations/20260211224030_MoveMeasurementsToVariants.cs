using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class MoveMeasurementsToVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMeasurements_Products_ProductId",
                table: "ProductMeasurements");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductMeasurements",
                newName: "ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMeasurements_ProductId",
                table: "ProductMeasurements",
                newName: "IX_ProductMeasurements_ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMeasurements_ProductVariants_ProductVariantId",
                table: "ProductMeasurements",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductMeasurements_ProductVariants_ProductVariantId",
                table: "ProductMeasurements");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "ProductMeasurements",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductMeasurements_ProductVariantId",
                table: "ProductMeasurements",
                newName: "IX_ProductMeasurements_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductMeasurements_Products_ProductId",
                table: "ProductMeasurements",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
