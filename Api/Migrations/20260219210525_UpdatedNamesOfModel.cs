using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNamesOfModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_StockItems_VariableAttributeId_Size",
                table: "StockItems",
                newName: "IX_StockItems_VariantId_Size");

            migrationBuilder.RenameColumn(
                name: "VariableAttributeId",
                table: "ImageGroups",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageGroups_VariableAttributeId",
                table: "ImageGroups",
                newName: "IX_ImageGroups_VariantId");

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "StockItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Garment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Neck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Warmth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Chest = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Neck = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SizeMeasurements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Garment_Material_Neck_Fit",
                table: "Products",
                columns: new[] { "Garment", "Material", "Neck", "Fit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SizeMeasurements_ProductId_Size",
                table: "SizeMeasurements",
                columns: new[] { "ProductId", "Size" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId_Color",
                table: "Variants",
                columns: new[] { "ProductId", "Color" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageGroups_Variants_VariantId",
                table: "ImageGroups",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Variants_VariantId",
                table: "StockItems",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageGroups_Variants_VariantId",
                table: "ImageGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Variants_VariantId",
                table: "StockItems");

            migrationBuilder.DropTable(
                name: "SizeMeasurements");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropColumn(
                name: "Sku",
                table: "StockItems");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "StockItems",
                newName: "VariableAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_StockItems_VariantId_Size",
                table: "StockItems",
                newName: "IX_StockItems_VariableAttributeId_Size");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "ImageGroups",
                newName: "VariableAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageGroups_VariantId",
                table: "ImageGroups",
                newName: "IX_ImageGroups_VariableAttributeId");

            migrationBuilder.CreateTable(
                name: "FixedAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Garment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Neck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    WarmthLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
    }
}
