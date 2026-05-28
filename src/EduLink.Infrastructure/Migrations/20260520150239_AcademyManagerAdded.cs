using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AcademyManagerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");



            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Academies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE Academies set ManagerId = (SELECT TOP 1 ID FROM AspNetUsers)");


            migrationBuilder.CreateIndex(
                name: "IX_Academies_ManagerId",
                table: "Academies",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Academies_AspNetUsers_ManagerId",
                table: "Academies",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Academies_AspNetUsers_ManagerId",
                table: "Academies");

            migrationBuilder.DropIndex(
                name: "IX_Academies_ManagerId",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Academies");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
