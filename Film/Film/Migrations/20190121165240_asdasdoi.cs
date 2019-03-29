using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class asdasdoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDates_AspNetUsers_UserRef",
                table: "UserDates");

            migrationBuilder.DropIndex(
                name: "IX_UserDates_UserRef",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "UserRef",
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

            migrationBuilder.AddColumn<string>(
                name: "UserRef",
                table: "UserDates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDates_UserRef",
                table: "UserDates",
                column: "UserRef",
                unique: true,
                filter: "[UserRef] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDates_AspNetUsers_UserRef",
                table: "UserDates",
                column: "UserRef",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
