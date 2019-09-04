using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class suscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SuscriptionId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Suscription",
                columns: table => new
                {
                    Endpoint = table.Column<string>(nullable: true),
                    P256DH = table.Column<string>(nullable: true),
                    Auth = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suscription", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SuscriptionId",
                table: "AspNetUsers",
                column: "SuscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Suscription_SuscriptionId",
                table: "AspNetUsers",
                column: "SuscriptionId",
                principalTable: "Suscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Suscription_SuscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Suscription");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SuscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuscriptionId",
                table: "AspNetUsers");
        }
    }
}
