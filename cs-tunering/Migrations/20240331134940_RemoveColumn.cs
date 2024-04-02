using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cs_tunering.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentTournamentId",
                table: "Tournaments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentTournamentId",
                table: "Tournaments",
                type: "int",
                nullable: true);
        }
    }
}
