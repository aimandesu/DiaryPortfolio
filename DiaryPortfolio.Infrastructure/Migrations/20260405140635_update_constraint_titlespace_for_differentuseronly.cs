using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_constraint_titlespace_for_differentuseronly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Spaces_DiaryProfileId",
                table: "Spaces");

            migrationBuilder.DropIndex(
                name: "IX_Spaces_Title",
                table: "Spaces");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_DiaryProfileId_Title",
                table: "Spaces",
                columns: new[] { "DiaryProfileId", "Title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Spaces_DiaryProfileId_Title",
                table: "Spaces");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_DiaryProfileId",
                table: "Spaces",
                column: "DiaryProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_Title",
                table: "Spaces",
                column: "Title",
                unique: true);
        }
    }
}
