using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentAssist.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrevisionPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prevision",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prevision",
                table: "Pacientes");
        }
    }
}
