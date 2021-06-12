using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
