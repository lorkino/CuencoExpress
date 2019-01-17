using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class k : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    Admin = table.Column<bool>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Puntuacion = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDates_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trabajo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Puntuacion = table.Column<double>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    TrabajadorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Trabajo_Id",
                        column: x => x.Id,
                        principalTable: "Trabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TrabajoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabajador_Trabajo_TrabajoId",
                        column: x => x.TrabajoId,
                        principalTable: "Trabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trabajador_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_UserId",
                table: "Cliente",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_TrabajoId",
                table: "Trabajador",
                column: "TrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_UserId",
                table: "Trabajador",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajo_TrabajadorId",
                table: "Trabajo",
                column: "TrabajadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajo_Cliente_TrabajadorId",
                table: "Trabajo",
                column: "TrabajadorId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Trabajo_Id",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "UserDates");

            migrationBuilder.DropTable(
                name: "Trabajo");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
