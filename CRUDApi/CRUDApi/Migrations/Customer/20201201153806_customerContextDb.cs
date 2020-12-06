using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDApi.Migrations.Customer
{
    public partial class customerContextDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orderDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
