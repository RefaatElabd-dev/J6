using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class removecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Cartid",
                table: "cart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cartid",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
