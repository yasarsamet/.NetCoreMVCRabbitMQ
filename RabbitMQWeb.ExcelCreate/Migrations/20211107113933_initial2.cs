using Microsoft.EntityFrameworkCore.Migrations;

namespace RabbitMQWeb.ExcelCreate.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UderId",
                table: "userFiles",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "userFiles",
                newName: "UderId");
        }
    }
}
