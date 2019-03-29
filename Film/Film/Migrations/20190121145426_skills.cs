using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class skills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Knowledges",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Explanation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserKnowledges",
                columns: table => new
                {
                    KnowledgesId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKnowledges", x => new { x.UserId, x.KnowledgesId });
                    table.ForeignKey(
                        name: "FK_UserKnowledges_Knowledges_KnowledgesId",
                        column: x => x.KnowledgesId,
                        principalTable: "Knowledges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserKnowledges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserKnowledges_KnowledgesId",
                table: "UserKnowledges",
                column: "KnowledgesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserKnowledges");

            migrationBuilder.DropTable(
                name: "Knowledges");
        }
    }
}
