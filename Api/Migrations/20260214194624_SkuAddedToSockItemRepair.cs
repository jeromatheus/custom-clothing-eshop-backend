using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class SkuAddedToSockItemRepair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fabric = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Neck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Warmth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ModelCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColorVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorVariants_ProductConfigurations_ProductConfigurationId",
                        column: x => x.ProductConfigurationId,
                        principalTable: "ProductConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SizeSpecifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChestWidthCm = table.Column<double>(type: "float", nullable: false),
                    TotalLengthCm = table.Column<double>(type: "float", nullable: false),
                    NeckCircumferenceCm = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SizeSpecifications_ProductConfigurations_ProductConfigurationId",
                        column: x => x.ProductConfigurationId,
                        principalTable: "ProductConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelHeight = table.Column<int>(type: "int", nullable: true),
                    ModelWearingSize = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageGroups_ColorVariants_ColorVariantId",
                        column: x => x.ColorVariantId,
                        principalTable: "ColorVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AvailableStock = table.Column<int>(type: "int", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockItems_ColorVariants_ColorVariantId",
                        column: x => x.ColorVariantId,
                        principalTable: "ColorVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ImageGroups_ImageGroupId",
                        column: x => x.ImageGroupId,
                        principalTable: "ImageGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorVariants_ProductConfigurationId_Color",
                table: "ColorVariants",
                columns: new[] { "ProductConfigurationId", "Color" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageGroups_ColorVariantId",
                table: "ImageGroups",
                column: "ColorVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageGroupId",
                table: "Images",
                column: "ImageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_ModelCode",
                table: "ProductConfigurations",
                column: "ModelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SizeSpecifications_ProductConfigurationId_Size",
                table: "SizeSpecifications",
                columns: new[] { "ProductConfigurationId", "Size" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_ColorVariantId_Size",
                table: "StockItems",
                columns: new[] { "ColorVariantId", "Size" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_Sku",
                table: "StockItems",
                column: "Sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "SizeSpecifications");

            migrationBuilder.DropTable(
                name: "StockItems");

            migrationBuilder.DropTable(
                name: "ImageGroups");

            migrationBuilder.DropTable(
                name: "ColorVariants");

            migrationBuilder.DropTable(
                name: "ProductConfigurations");
        }
    }
}
