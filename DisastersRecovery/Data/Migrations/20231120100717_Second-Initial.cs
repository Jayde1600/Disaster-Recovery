using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisastersRecovery.Data.Migrations
{
    public partial class SecondInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllocateFunds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DisasterId = table.Column<int>(type: "int", nullable: false),
                    AllocationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocateFunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocateFunds_DisasterCheck_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "DisasterCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllocateGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DisasterId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AllocationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocateGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocateGoods_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocateGoods_DisasterCheck_DisasterId",
                        column: x => x.DisasterId,
                        principalTable: "DisasterCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoodsId = table.Column<int>(type: "int", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableGoods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvailableMoney",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AmountUsed = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableMoney", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AmountUsed = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseGoods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocateFunds_DisasterId",
                table: "AllocateFunds",
                column: "DisasterId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateGoods_CategoryId",
                table: "AllocateGoods",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocateGoods_DisasterId",
                table: "AllocateGoods",
                column: "DisasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocateFunds");

            migrationBuilder.DropTable(
                name: "AllocateGoods");

            migrationBuilder.DropTable(
                name: "AvailableGoods");

            migrationBuilder.DropTable(
                name: "AvailableMoney");

            migrationBuilder.DropTable(
                name: "PurchaseGoods");
        }
    }
}
