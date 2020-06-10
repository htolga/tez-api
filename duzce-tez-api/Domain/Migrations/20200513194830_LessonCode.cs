using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class LessonCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Lesson",
                table: "Lesson",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Lesson",
                table: "Lesson");
        }
    }
}
