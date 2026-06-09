using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AcademyLogoUrlAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Academies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Academies");
        }
    }
}
