using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class ModelUnificationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageGroups_ColorVariants_ColorVariantId",
                table: "ImageGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_ColorVariants_ColorVariantId",
                table: "StockItems");

            migrationBuilder.DropTable(
                name: "ColorVariants");

            migrationBuilder.DropTable(
                name: "SizeSpecifications");

            migrationBuilder.DropTable(
                name: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_Sku",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "StockItems");

            migrationBuilder.RenameColumn(
                name: "ColorVariantId",
                table: "StockItems",
                newName: "VariableAttributeId");

            migrationBuilder.RenameColumn(
                name: "AvailableStock",
                table: "StockItems",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_StockItems_ColorVariantId_Size",
                table: "StockItems",
                newName: "IX_StockItems_VariableAttributeId_Size");

            migrationBuilder.RenameColumn(
                name: "ColorVariantId",
                table: "ImageGroups",
                newName: "VariableAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageGroups_ColorVariantId",
                table: "ImageGroups",
                newName: "IX_ImageGroups_VariableAttributeId");

            migrationBuilder.CreateTable(
                name: "FixedAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Garment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Neck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VariableAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FixedAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariableAttributes_FixedAttributes_FixedAttributeId",
                        column: x => x.FixedAttributeId,
                        principalTable: "FixedAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FixedAttributes_Garment_Material_Neck_Fit",
                table: "FixedAttributes",
                columns: new[] { "Garment", "Material", "Neck", "Fit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariableAttributes_FixedAttributeId_Color",
                table: "VariableAttributes",
                columns: new[] { "FixedAttributeId", "Color" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageGroups_VariableAttributes_VariableAttributeId",
                table: "ImageGroups",
                column: "VariableAttributeId",
                principalTable: "VariableAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_VariableAttributes_VariableAttributeId",
                table: "StockItems",
                column: "VariableAttributeId",
                principalTable: "VariableAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageGroups_VariableAttributes_VariableAttributeId",
                table: "ImageGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_VariableAttributes_VariableAttributeId",
                table: "StockItems");

            migrationBuilder.DropTable(
                name: "VariableAttributes");

            migrationBuilder.DropTable(
                name: "FixedAttributes");

            migrationBuilder.RenameColumn(
                name: "VariableAttributeId",
                table: "StockItems",
                newName: "ColorVariantId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StockItems",
                newName: "AvailableStock");

            migrationBuilder.RenameIndex(
                name: "IX_StockItems_VariableAttributeId_Size",
                table: "StockItems",
                newName: "IX_StockItems_ColorVariantId_Size");

            migrationBuilder.RenameColumn(
                name: "VariableAttributeId",
                table: "ImageGroups",
                newName: "ColorVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageGroups_VariableAttributeId",
                table: "ImageGroups",
                newName: "IX_ImageGroups_ColorVariantId");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "StockItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fabric = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Neck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Warmth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ChestWidthCm = table.Column<double>(type: "float", nullable: false),
                    NeckCircumferenceCm = table.Column<double>(type: "float", nullable: false),
                    ProductConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalLengthCm = table.Column<double>(type: "float", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_Sku",
                table: "StockItems",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorVariants_ProductConfigurationId_Color",
                table: "ColorVariants",
                columns: new[] { "ProductConfigurationId", "Color" },
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_ImageGroups_ColorVariants_ColorVariantId",
                table: "ImageGroups",
                column: "ColorVariantId",
                principalTable: "ColorVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_ColorVariants_ColorVariantId",
                table: "StockItems",
                column: "ColorVariantId",
                principalTable: "ColorVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
