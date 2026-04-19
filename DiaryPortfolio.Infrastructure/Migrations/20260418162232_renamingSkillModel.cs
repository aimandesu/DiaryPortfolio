using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamingSkillModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SelectionId",
                table: "Skills",
                column: "SelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Selections_SelectionId",
                table: "Skills",
                column: "SelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Selections_SelectionId",
                table: "Skills");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Selections_SkillLevelId",
                table: "Skills",
                column: "SkillLevelId",
                principalTable: "Selections",
                principalColumn: "Id");
        }
    }
}
