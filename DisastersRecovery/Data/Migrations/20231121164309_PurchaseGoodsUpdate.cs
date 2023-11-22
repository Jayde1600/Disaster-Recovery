using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisastersRecovery.Data.Migrations
{
    public partial class PurchaseGoodsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PurchaseGoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGoods_CategoryId",
                table: "PurchaseGoods",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseGoods_Categories_CategoryId",
                table: "PurchaseGoods",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseGoods_Categories_CategoryId",
                table: "PurchaseGoods");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseGoods_CategoryId",
                table: "PurchaseGoods");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PurchaseGoods");
        }
    }
}
