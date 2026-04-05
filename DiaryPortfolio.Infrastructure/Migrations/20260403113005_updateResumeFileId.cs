using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateResumeFileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResumeFileId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResumeTemplateId",
                table: "Resume",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeFileId",
                table: "Resume",
                column: "ResumeFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_Files_ResumeFileId",
                table: "Resume",
                column: "ResumeFileId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume",
                column: "ResumeTemplateId",
                principalTable: "ResumeTemplate",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_Files_ResumeFileId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_Resume_ResumeTemplate_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ResumeFileId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_ResumeTemplateId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeFileId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "ResumeTemplateId",
                table: "Resume");
        }
    }
}
