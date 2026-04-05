using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_projectmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Selections_ProjectTypeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectTypeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SelectionId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectModelId",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectModelId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ProjectModelId",
                table: "Videos",
                column: "ProjectModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProjectModelId",
                table: "Photos",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Projects_ProjectModelId",
                table: "Photos",
                column: "ProjectModelId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Projects_ProjectModelId",
                table: "Videos",
                column: "ProjectModelId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Projects_ProjectModelId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Projects_ProjectModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ProjectModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProjectModelId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "Photos");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectTypeId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SelectionId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectTypeId",
                table: "Projects",
                column: "ProjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Selections_ProjectTypeId",
                table: "Projects",
                column: "ProjectTypeId",
                principalTable: "Selections",
                principalColumn: "Id");
        }
    }
}
