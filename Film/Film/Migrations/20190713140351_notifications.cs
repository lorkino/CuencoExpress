using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Suscription_SuscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suscription",
                table: "Suscription");

            migrationBuilder.DropColumn(
                name: "Auth",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Endpoint",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "P256DH",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Suscription",
                newName: "Suscriptions");

            migrationBuilder.AddColumn<bool>(
                name: "Readed",
                table: "Notifications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suscriptions",
                table: "Suscriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Suscriptions_SuscriptionId",
                table: "AspNetUsers",
                column: "SuscriptionId",
                principalTable: "Suscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Suscriptions_SuscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suscriptions",
                table: "Suscriptions");

            migrationBuilder.DropColumn(
                name: "Readed",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Suscriptions",
                newName: "Suscription");

            migrationBuilder.AddColumn<string>(
                name: "Auth",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endpoint",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "P256DH",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suscription",
                table: "Suscription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Suscription_SuscriptionId",
                table: "AspNetUsers",
                column: "SuscriptionId",
                principalTable: "Suscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
