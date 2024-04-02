using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cs_tunering.Migrations
{
    /// <inheritdoc />
    public partial class ChildTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildTournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerCount = table.Column<int>(type: "int", nullable: true),
                    ParentTournamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildTournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildTournaments_Tournaments_ParentTournamentId",
                        column: x => x.ParentTournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildTournaments_ParentTournamentId",
                table: "ChildTournaments",
                column: "ParentTournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildTournaments");
        }
    }
}
