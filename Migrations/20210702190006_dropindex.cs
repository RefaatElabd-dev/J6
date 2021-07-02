using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class dropindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cart_CustimerId",
                table: "cart" );

            migrationBuilder.DropIndex(
               name: "IX_Orders_CustimerId",
                table: "Orders" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
