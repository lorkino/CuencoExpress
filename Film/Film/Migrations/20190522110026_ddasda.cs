using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class ddasda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Job_JobId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_JobId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "JobId1",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "JobImages",
                table: "Job",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobImages",
                table: "Job");

            migrationBuilder.AddColumn<string>(
                name: "JobId1",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_JobId1",
                table: "Images",
                column: "JobId1");

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
