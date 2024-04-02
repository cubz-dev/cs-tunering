using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cs_tunering.Migrations
{
    /// <inheritdoc />
    public partial class IsParentTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsParentTournament",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsParentTournament",
                table: "Tournaments");
        }
    }
}
