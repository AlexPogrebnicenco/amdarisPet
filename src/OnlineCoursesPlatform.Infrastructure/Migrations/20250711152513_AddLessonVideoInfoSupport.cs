using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursesPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonVideoInfoSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrls",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrls",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]"); // пустой массив в формате JSON
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrls",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrls",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }
    }
}
