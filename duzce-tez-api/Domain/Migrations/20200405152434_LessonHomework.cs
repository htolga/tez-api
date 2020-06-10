using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class LessonHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lessonHomeworks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FileKey = table.Column<Guid>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonHomeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonHomeworks_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalSchema: "Lesson",
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentHomeworks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FileKey = table.Column<Guid>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    LessonHomeworkId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentHomeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentHomeworks_lessonHomeworks_LessonHomeworkId",
                        column: x => x.LessonHomeworkId,
                        principalTable: "lessonHomeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentHomeworks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonHomeworks_LessonId",
                table: "lessonHomeworks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHomeworks_LessonHomeworkId",
                table: "StudentHomeworks",
                column: "LessonHomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentHomeworks_UserId",
                table: "StudentHomeworks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentHomeworks");

            migrationBuilder.DropTable(
                name: "lessonHomeworks");
        }
    }
}
