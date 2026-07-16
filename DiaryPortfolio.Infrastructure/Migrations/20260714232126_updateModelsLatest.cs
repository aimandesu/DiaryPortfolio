using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateModelsLatest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop the blocking Foreign Key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Selections_SkillLevelId",
                table: "Skills");

            // 2. Drop the blocking Database Index
            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillLevelId",
                table: "Skills");
            
            migrationBuilder.DropColumn(
                name: "SkillLevelId",
                table: "Skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SkillLevelId",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: true);
            
            // Recreate index on rollback
            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillLevelId",
                table: "Skills",
                column: "SkillLevelId");

            // Recreate foreign key on rollback
            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Selections_SkillLevelId",
                table: "Skills",
                column: "SkillLevelId",
                principalTable: "Selections",
                principalColumn: "Id");
        }
    }
}
