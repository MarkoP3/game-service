using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "game");

            migrationBuilder.CreateTable(
                name: "choices",
                schema: "game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceChoice",
                schema: "game",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(type: "int", nullable: false),
                    WeakerChoicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceChoice", x => new { x.ChoiceId, x.WeakerChoicesId });
                    table.ForeignKey(
                        name: "FK_ChoiceChoice_choices_ChoiceId",
                        column: x => x.ChoiceId,
                        principalSchema: "game",
                        principalTable: "choices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChoiceChoice_choices_WeakerChoicesId",
                        column: x => x.WeakerChoicesId,
                        principalSchema: "game",
                        principalTable: "choices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceChoice_WeakerChoicesId",
                schema: "game",
                table: "ChoiceChoice",
                column: "WeakerChoicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoiceChoice",
                schema: "game");

            migrationBuilder.DropTable(
                name: "choices",
                schema: "game");
        }
    }
}
