using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class sellerinchat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sellerId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sellerId",
                table: "Messages");
        }
    }
}
