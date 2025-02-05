using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CybontrolX.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employee",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Employee",
                newName: "FullName");
        }
    }
}
