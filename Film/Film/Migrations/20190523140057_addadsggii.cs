using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class addadsggii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_JobId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Job_JobId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_JobId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "JobId1",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Job_JobId",
                table: "Images",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Job_JobId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "JobId1",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_JobId1",
                table: "Images",
                column: "JobId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_JobId",
                table: "Images",
                column: "JobId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Job_JobId1",
                table: "Images",
                column: "JobId1",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
