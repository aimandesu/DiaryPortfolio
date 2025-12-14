using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTextIdUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TextId",
                table: "Medias");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TextId",
                table: "Medias",
                column: "TextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias",
                column: "TextId",
                principalTable: "TextStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TextId",
                table: "Medias");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_TextId",
                table: "Medias",
                column: "TextId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias",
                column: "TextId",
                principalTable: "TextStyle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
