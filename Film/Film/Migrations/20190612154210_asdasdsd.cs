using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class asdasdsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Job_PreworkersInJobId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PreworkersInJobId",
                table: "AspNetUsers",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PreworkersInJobId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Job_JobId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "AspNetUsers",
                newName: "PreworkersInJobId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_JobId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PreworkersInJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Job_PreworkersInJobId",
                table: "AspNetUsers",
                column: "PreworkersInJobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
