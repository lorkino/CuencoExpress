using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Job_JobPreWorkerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JobPreWorkerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobPreWorkerId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "JobPreWorkers",
                columns: table => new
                {
                    JobId = table.Column<string>(nullable: false),
                    UserPreWorkeId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPreWorkers", x => new { x.JobId, x.UserPreWorkeId });
                    table.ForeignKey(
                        name: "FK_JobPreWorkers_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPreWorkers_AspNetUsers_UserPreWorkeId",
                        column: x => x.UserPreWorkeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPreWorkers_UserPreWorkeId",
                table: "JobPreWorkers",
                column: "UserPreWorkeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPreWorkers");

            migrationBuilder.AddColumn<string>(
                name: "JobPreWorkerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JobPreWorkerId",
                table: "AspNetUsers",
                column: "JobPreWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Job_JobPreWorkerId",
                table: "AspNetUsers",
                column: "JobPreWorkerId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
