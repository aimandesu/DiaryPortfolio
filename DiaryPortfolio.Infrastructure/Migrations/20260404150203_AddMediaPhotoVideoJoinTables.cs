using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaPhotoVideoJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Medias_MediaId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Projects_ProjectModelId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Medias_MediaId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Projects_ProjectModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_MediaId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ProjectModelId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MediaId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProjectModelId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "Photos");

            migrationBuilder.CreateTable(
                name: "MediaPhotoModel",
                columns: table => new
                {
                    MediaModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPhotoModel", x => new { x.MediaModelId, x.PhotoModelId });
                    table.ForeignKey(
                        name: "FK_MediaPhotoModel_Medias_MediaModelId",
                        column: x => x.MediaModelId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaPhotoModel_Photos_PhotoModelId",
                        column: x => x.PhotoModelId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MediaVideoModel",
                columns: table => new
                {
                    MediaModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaVideoModel", x => new { x.MediaModelId, x.VideoModelId });
                    table.ForeignKey(
                        name: "FK_MediaVideoModel_Medias_MediaModelId",
                        column: x => x.MediaModelId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaVideoModel_Videos_VideoModelId",
                        column: x => x.VideoModelId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhotoModel",
                columns: table => new
                {
                    ProjectModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhotoModel", x => new { x.ProjectModelId, x.PhotoModelId });
                    table.ForeignKey(
                        name: "FK_ProjectPhotoModel_Photos_PhotoModelId",
                        column: x => x.PhotoModelId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPhotoModel_Projects_ProjectModelId",
                        column: x => x.ProjectModelId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVideoModel",
                columns: table => new
                {
                    ProjectModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVideoModel", x => new { x.ProjectModelId, x.VideoModelId });
                    table.ForeignKey(
                        name: "FK_ProjectVideoModel_Projects_ProjectModelId",
                        column: x => x.ProjectModelId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectVideoModel_Videos_VideoModelId",
                        column: x => x.VideoModelId,
                        principalTable: "Videos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaPhotoModel_PhotoModelId",
                table: "MediaPhotoModel",
                column: "PhotoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaVideoModel_VideoModelId",
                table: "MediaVideoModel",
                column: "VideoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhotoModel_PhotoModelId",
                table: "ProjectPhotoModel",
                column: "PhotoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVideoModel_VideoModelId",
                table: "ProjectVideoModel",
                column: "VideoModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaPhotoModel");

            migrationBuilder.DropTable(
                name: "MediaVideoModel");

            migrationBuilder.DropTable(
                name: "ProjectPhotoModel");

            migrationBuilder.DropTable(
                name: "ProjectVideoModel");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaId",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectModelId",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MediaId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectModelId",
                table: "Photos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MediaId",
                table: "Videos",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ProjectModelId",
                table: "Videos",
                column: "ProjectModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MediaId",
                table: "Photos",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProjectModelId",
                table: "Photos",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Medias_MediaId",
                table: "Photos",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Projects_ProjectModelId",
                table: "Photos",
                column: "ProjectModelId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Medias_MediaId",
                table: "Videos",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Projects_ProjectModelId",
                table: "Videos",
                column: "ProjectModelId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
