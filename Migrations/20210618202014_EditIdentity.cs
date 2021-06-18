using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class EditIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_ShippingDetails",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_prod_Cart_product",
                table: "prod_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_prod_order_Orders",
                table: "prod_order");

            migrationBuilder.DropForeignKey(
                name: "FK_prod_order_product",
                table: "prod_order");

            migrationBuilder.DropForeignKey(
                name: "FK_product_Brands_BrandId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_Promotions",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_product_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBag_product_ProductId",
                table: "ProductsBag");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_product",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_payment",
                table: "ShippingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_product",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Stores",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_product",
                table: "Views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingDetails",
                table: "ShippingDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment",
                table: "payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                table: "product");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "storeId",
                table: "Stores",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "statusId",
                table: "Statuses",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "shippingDetailsId",
                table: "ShippingDetails",
                newName: "ShippingDetailsId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Promotions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "paymentId",
                table: "payment",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "orders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_product_subcategoryId",
                table: "Products",
                newName: "IX_Products_subcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_product_promotionId",
                table: "Products",
                newName: "IX_Products_promotionId");

            migrationBuilder.RenameIndex(
                name: "IX_product_BrandId",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShippingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "payment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "Products",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingDetails",
                table: "ShippingDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "PromotionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment",
                table: "payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_ShippingDetailsId",
                table: "ShippingDetails",
                column: "ShippingDetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_ShippingDetails",
                table: "cart",
                column: "shippingDetailsId",
                principalTable: "ShippingDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_prod_Cart_product",
                table: "prod_Cart",
                column: "cartId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_prod_order_Orders",
                table: "prod_order",
                column: "orderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_prod_order_product",
                table: "prod_order",
                column: "orderId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_Promotions",
                table: "Products",
                column: "promotionId",
                principalTable: "Promotions",
                principalColumn: "PromotionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBag_Products_ProductId",
                table: "ProductsBag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_product",
                table: "Reviews",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_payment",
                table: "ShippingDetails",
                column: "paymentId",
                principalTable: "payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails",
                column: "ShippingDetailsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_product",
                table: "StoreProducts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Stores",
                table: "StoreProducts",
                column: "storeId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_product",
                table: "Views",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_ShippingDetails",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_prod_Cart_product",
                table: "prod_Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_prod_order_Orders",
                table: "prod_order");

            migrationBuilder.DropForeignKey(
                name: "FK_prod_order_product",
                table: "prod_order");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_product_Promotions",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsBag_Products_ProductId",
                table: "ProductsBag");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_product",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_payment",
                table: "ShippingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_product",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Stores",
                table: "StoreProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_product",
                table: "Views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingDetails",
                table: "ShippingDetails");

            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_ShippingDetailsId",
                table: "ShippingDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment",
                table: "payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "product");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Stores",
                newName: "storeId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Statuses",
                newName: "statusId");

            migrationBuilder.RenameColumn(
                name: "ShippingDetailsId",
                table: "ShippingDetails",
                newName: "shippingDetailsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Promotions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "payment",
                newName: "paymentId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "orders",
                newName: "orderId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "product",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_subcategoryId",
                table: "product",
                newName: "IX_product_subcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_promotionId",
                table: "product",
                newName: "IX_product_promotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "product",
                newName: "IX_product_BrandId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "product",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "storeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "statusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingDetails",
                table: "ShippingDetails",
                column: "shippingDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Promotions",
                table: "Promotions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment",
                table: "payment",
                column: "paymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "orderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                table: "product",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_ShippingDetails",
                table: "cart",
                column: "shippingDetailsId",
                principalTable: "ShippingDetails",
                principalColumn: "shippingDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_prod_Cart_product",
                table: "prod_Cart",
                column: "cartId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_prod_order_Orders",
                table: "prod_order",
                column: "orderId",
                principalTable: "orders",
                principalColumn: "orderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_prod_order_product",
                table: "prod_order",
                column: "orderId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_Brands_BrandId",
                table: "product",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_Promotions",
                table: "product",
                column: "promotionId",
                principalTable: "Promotions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_product_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsBag_product_ProductId",
                table: "ProductsBag",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_product",
                table: "Reviews",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_payment",
                table: "ShippingDetails",
                column: "paymentId",
                principalTable: "payment",
                principalColumn: "paymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails",
                column: "shippingDetailsId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_product",
                table: "StoreProducts",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Stores",
                table: "StoreProducts",
                column: "storeId",
                principalTable: "Stores",
                principalColumn: "storeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_product",
                table: "Views",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
