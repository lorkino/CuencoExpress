using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class adsasd2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Long",
                table: "UserDates",
                newName: "Lon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lon",
                table: "UserDates",
                newName: "Long");
        }
    }
}
