using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class DeleteUnsedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails");

            migrationBuilder.DropIndex(
                name: "IX_ShippingDetails_ShippingDetailsId",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ShippingDetailsId",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "payment");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShippingDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails",
                column: "Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShippingDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ShippingDetailsId",
                table: "ShippingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Promotions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_ShippingDetailsId",
                table: "ShippingDetails",
                column: "ShippingDetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingDetails_product",
                table: "ShippingDetails",
                column: "ShippingDetailsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
