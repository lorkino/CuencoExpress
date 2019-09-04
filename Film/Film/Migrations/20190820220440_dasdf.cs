using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class dasdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Lat",
                table: "UserDates",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Long",
                table: "UserDates",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "UserDates");
        }
    }
}
