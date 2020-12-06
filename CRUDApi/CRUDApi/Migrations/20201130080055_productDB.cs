using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDApi.Migrations
{
    public partial class productDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "winformProduct",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productPrice = table.Column<int>(type: "int", nullable: false),
                    topSale = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winformProduct", x => x.productId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "winformProduct");
        }
    }
}
