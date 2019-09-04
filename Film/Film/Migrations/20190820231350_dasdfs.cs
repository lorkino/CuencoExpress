using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class dasdfs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Long",
                table: "UserDates",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "UserDates",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Long",
                table: "UserDates",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "UserDates",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
