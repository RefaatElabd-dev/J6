using Microsoft.EntityFrameworkCore.Migrations;

namespace J6.Migrations
{
    public partial class AddedSavedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedBag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedBag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedBag_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsBag",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SaveBagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsBag", x => new { x.ProductId, x.SaveBagId });
                    table.ForeignKey(
                        name: "FK_ProductsBag_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsBag_SavedBag_SaveBagId",
                        column: x => x.SaveBagId,
                        principalTable: "SavedBag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsBag_SaveBagId",
                table: "ProductsBag",
                column: "SaveBagId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedBag_CustomerId",
                table: "SavedBag",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsBag");

            migrationBuilder.DropTable(
                name: "SavedBag");
        }
    }
}
