using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class asaddd8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalInfo",
                table: "UserDates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalInfo",
                table: "UserDates");
        }
    }
}
