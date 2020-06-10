using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class FullLessonDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "Lesson",
                table: "LessonDocument",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "Lesson",
                table: "LessonDocument",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "Lesson",
                table: "LessonDocument");

            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "Lesson",
                table: "LessonDocument");
        }
    }
}
