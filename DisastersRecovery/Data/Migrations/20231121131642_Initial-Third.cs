using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisastersRecovery.Data.Migrations
{
    public partial class InitialThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoodsId",
                table: "AvailableGoods",
                newName: "QuantityUsed");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "AvailableGoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AvailableGoods_CategoryId",
                table: "AvailableGoods",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableGoods_Categories_CategoryId",
                table: "AvailableGoods",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableGoods_Categories_CategoryId",
                table: "AvailableGoods");

            migrationBuilder.DropIndex(
                name: "IX_AvailableGoods_CategoryId",
                table: "AvailableGoods");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AvailableGoods");

            migrationBuilder.RenameColumn(
                name: "QuantityUsed",
                table: "AvailableGoods",
                newName: "GoodsId");
        }
    }
}
