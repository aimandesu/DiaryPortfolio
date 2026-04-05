using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove_tabletext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medias_TextStyle_TextId",
                table: "Medias");

            migrationBuilder.DropTable(
                name: "TextStyle");

            migrationBuilder.DropIndex(
                name: "IX_Medias_TextId",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "TextId",
                table: "Medias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TextId",
                table: "Medias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TextStyle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FontSize = table.Column<int>(type: "int", nullable: false),
                    TextStyle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextStyle", x => x.Id);
                });

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
    }
}
