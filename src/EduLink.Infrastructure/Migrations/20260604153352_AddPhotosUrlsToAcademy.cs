using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotosUrlsToAcademy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotosUrls",
                table: "Academies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotosUrls",
                table: "Academies");
        }
    }
}
