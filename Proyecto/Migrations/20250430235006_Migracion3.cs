using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    /// <inheritdoc />
    public partial class Migracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyUse",
                table: "Dieta");

            migrationBuilder.AddColumn<int>(
                name: "TotalCalories",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Usuario");

            migrationBuilder.AddColumn<int>(
                name: "DailyUse",
                table: "Dieta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
