using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class BrandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "product");

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    brandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brandName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "date", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand", x => x.brandId);
                });

            migrationBuilder.CreateTable(
                name: "productBrand",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false),
                    brandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productBrand", x => new { x.productId, x.brandId });
                    table.ForeignKey(
                        name: "FK_productBrand_brand",
                        column: x => x.brandId,
                        principalTable: "brand",
                        principalColumn: "brandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_productBrand_product",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productBrand_brandId",
                table: "productBrand",
                column: "brandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productBrand");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
