using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class TCKN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TCKN",
                schema: "User",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TCKN",
                schema: "User",
                table: "AspNetUsers");
        }
    }
}
