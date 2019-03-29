using Microsoft.EntityFrameworkCore.Migrations;

namespace Film.Migrations
{
    public partial class aaqpio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobKnowledges_Job_JobId",
                table: "JobKnowledges");

            migrationBuilder.DropForeignKey(
                name: "FK_JobKnowledges_Knowledges_KnowledgesId",
                table: "JobKnowledges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobKnowledges",
                table: "JobKnowledges");

            migrationBuilder.DropIndex(
                name: "IX_JobKnowledges_JobId",
                table: "JobKnowledges");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JobKnowledges");

            migrationBuilder.AlterColumn<string>(
                name: "KnowledgesId",
                table: "JobKnowledges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobId",
                table: "JobKnowledges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobKnowledges",
                table: "JobKnowledges",
                columns: new[] { "JobId", "KnowledgesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JobKnowledges_Job_JobId",
                table: "JobKnowledges",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobKnowledges_Knowledges_KnowledgesId",
                table: "JobKnowledges",
                column: "KnowledgesId",
                principalTable: "Knowledges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobKnowledges_Job_JobId",
                table: "JobKnowledges");

            migrationBuilder.DropForeignKey(
                name: "FK_JobKnowledges_Knowledges_KnowledgesId",
                table: "JobKnowledges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobKnowledges",
                table: "JobKnowledges");

            migrationBuilder.AlterColumn<string>(
                name: "KnowledgesId",
                table: "JobKnowledges",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "JobId",
                table: "JobKnowledges",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "JobKnowledges",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobKnowledges",
                table: "JobKnowledges",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobKnowledges_JobId",
                table: "JobKnowledges",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobKnowledges_Job_JobId",
                table: "JobKnowledges",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobKnowledges_Knowledges_KnowledgesId",
                table: "JobKnowledges",
                column: "KnowledgesId",
                principalTable: "Knowledges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
