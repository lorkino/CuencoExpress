using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class asdasda4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Job_JobId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JobId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "JobPreWorker",
                columns: table => new
                {
                    JobId = table.Column<string>(nullable: false),
                    UserPreWorkeId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPreWorker", x => new { x.JobId, x.UserPreWorkeId });
                    table.ForeignKey(
                        name: "FK_JobPreWorker_AspNetUsers_JobId",
                        column: x => x.JobId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPreWorker_Job_UserPreWorkeId",
                        column: x => x.UserPreWorkeId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPreWorker_UserPreWorkeId",
                table: "JobPreWorker",
                column: "UserPreWorkeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPreWorker");

            migrationBuilder.AddColumn<string>(
                name: "JobId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JobId",
                table: "AspNetUsers",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Job_JobId",
                table: "AspNetUsers",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
