using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addActiivationCodeinUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "Users");
        }
    }
}
