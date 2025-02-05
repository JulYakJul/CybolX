using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CybontrolX.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTariff1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TariffName",
                table: "Tariffs",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Tariffs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Tariffs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tariffs",
                newName: "TariffName");
        }
    }
}
