using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisastersRecovery.Data.Migrations
{
    public partial class ActiveDisasters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveDisasters",
                columns: table => new
                {
                    DisasterCheckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disaster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllocatedAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AllocatedQuantity = table.Column<int>(type: "int", nullable: false),
                    GoodsCategory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveDisasters", x => x.DisasterCheckId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveDisasters");
        }
    }
}
