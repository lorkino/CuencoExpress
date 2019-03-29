using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class aaqp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Trabajo_Id",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Trabajo");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserCreatorId = table.Column<string>(nullable: true),
                    UserWorkerId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Tittle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_AspNetUsers_UserCreatorId",
                        column: x => x.UserCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Job_AspNetUsers_UserWorkerId",
                        column: x => x.UserWorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Img = table.Column<byte[]>(nullable: true),
                    JobId = table.Column<string>(nullable: true),
                    JobId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AspNetUsers_JobId",
                        column: x => x.JobId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Job_JobId1",
                        column: x => x.JobId1,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobKnowledges",
                columns: table => new
                {
                    KnowledgesId = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    JobId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobKnowledges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobKnowledges_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobKnowledges_Knowledges_KnowledgesId",
                        column: x => x.KnowledgesId,
                        principalTable: "Knowledges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_JobId",
                table: "Images",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_JobId1",
                table: "Images",
                column: "JobId1");

            migrationBuilder.CreateIndex(
                name: "IX_Job_UserCreatorId",
                table: "Job",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_UserWorkerId",
                table: "Job",
                column: "UserWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobKnowledges_JobId",
                table: "JobKnowledges",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobKnowledges_KnowledgesId",
                table: "JobKnowledges",
                column: "KnowledgesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "JobKnowledges");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.CreateTable(
                name: "Trabajo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Puntuacion = table.Column<double>(nullable: false),
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
                        name: "FK_Cliente_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TrabajoId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
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
                        name: "FK_Trabajador_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
    }
}
