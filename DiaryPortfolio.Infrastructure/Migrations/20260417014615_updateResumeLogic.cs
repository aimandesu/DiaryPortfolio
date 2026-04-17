using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateResumeLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resume_FileId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_TemplateId",
                table: "Resume");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_FileId",
                table: "Resume",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_TemplateId",
                table: "Resume",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resume_FileId",
                table: "Resume");

            migrationBuilder.DropIndex(
                name: "IX_Resume_TemplateId",
                table: "Resume");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_FileId",
                table: "Resume",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_TemplateId",
                table: "Resume",
                column: "TemplateId",
                unique: true,
                filter: "[TemplateId] IS NOT NULL");
        }
    }
}
