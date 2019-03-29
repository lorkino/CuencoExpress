using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class cambios2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_AspNetUsers_Id",
                table: "UserDates");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_AspNetUsers_Id",
                table: "UserDates",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_AspNetUsers_Id",
                table: "UserDates");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_AspNetUsers_Id",
                table: "UserDates",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
