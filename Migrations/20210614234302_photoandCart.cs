using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class photoandCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prod_Cart_cart",
                table: "prod_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_product",
                table: "ProductImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                table: "cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "cartid",
                table: "cart",
                newName: "Cartid");

            migrationBuilder.RenameColumn(
                name: "imageId",
                table: "ProductImages",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "ProductImages",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "ProductImages",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ImId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                table: "cart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "ImId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_prod_Cart_cart",
                table: "prod_Cart",
                column: "cartId",
                principalTable: "cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_product_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prod_Cart_cart",
                table: "prod_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_product_ProductId",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart",
                table: "cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "ImId",
                table: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "ProductImage");

            migrationBuilder.RenameColumn(
                name: "Cartid",
                table: "cart",
                newName: "cartid");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductImage",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ProductImage",
                newName: "imageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImage",
                newName: "image");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart",
                table: "cart",
                column: "cartid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                columns: new[] { "productId", "imageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_prod_Cart_cart",
                table: "prod_Cart",
                column: "cartId",
                principalTable: "cart",
                principalColumn: "cartid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_product",
                table: "ProductImage",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
