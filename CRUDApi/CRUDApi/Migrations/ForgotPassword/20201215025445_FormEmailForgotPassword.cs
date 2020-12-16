using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDApi.Migrations.ForgotPassword
{
    public partial class FormEmailForgotPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormHtmlForgotPassword",
                columns: table => new
                {
                    Subject = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHtmlForgotPassword", x => x.Subject);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormHtmlForgotPassword");
        }
    }
}
