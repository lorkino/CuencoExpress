using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class cambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "UserDates",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Puntuacion",
                table: "UserDates",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "UserDates",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "AspNetUsers",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "UserDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "UserDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "UserDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "UserDates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "UserDates");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "UserDates");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "UserDates",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "UserDates",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "UserDates",
                newName: "Puntuacion");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AspNetUsers",
                newName: "Estado");

            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
