using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class HomeworkDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileKey",
                table: "lessonHomeworks");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "lessonHomeworks");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "lessonHomeworks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileKey",
                table: "lessonHomeworks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "lessonHomeworks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "lessonHomeworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
