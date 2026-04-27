using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEducationNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Selections_EducationTierId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_EducationTierId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "EducationTierId",
                table: "Educations");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_SelectionId",
                table: "Educations",
                column: "SelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Selections_SelectionId",
                table: "Educations",
                column: "SelectionId",
                principalTable: "Selections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Selections_SelectionId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_SelectionId",
                table: "Educations");

            migrationBuilder.AddColumn<Guid>(
                name: "EducationTierId",
                table: "Educations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_EducationTierId",
                table: "Educations",
                column: "EducationTierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Selections_EducationTierId",
                table: "Educations",
                column: "EducationTierId",
                principalTable: "Selections",
                principalColumn: "Id");
        }
    }
}
