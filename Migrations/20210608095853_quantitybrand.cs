using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class quantitybrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "product");

            migrationBuilder.AlterColumn<double>(
                name: "price",
                table: "product",
                type: "float",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "prod_Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_BrandId",
                table: "product",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_Brand_BrandId",
                table: "product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_Brand_BrandId",
                table: "product");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_product_BrandId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "prod_Cart");

            migrationBuilder.AlterColumn<string>(
                name: "price",
                table: "product",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
