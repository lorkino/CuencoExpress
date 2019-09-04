using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class asdasdsdgodgr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Job_JobId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "AspNetUsers",
                newName: "JobPreWorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_JobId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_JobPreWorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Job_JobPreWorkerId",
                table: "AspNetUsers",
                column: "JobPreWorkerId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Job_JobPreWorkerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "JobPreWorkerId",
                table: "AspNetUsers",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_JobPreWorkerId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_JobId");

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
