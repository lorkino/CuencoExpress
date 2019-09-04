using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class unsuscribe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Suscribed",
                table: "UserDates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suscribed",
                table: "UserDates");
        }
    }
}
