using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiaryPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addChatModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectTypeModel_ProjectTypeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypeModel_PortfolioProfile_PortfolioProfileId",
                table: "ProjectTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTypeModel",
                table: "ProjectTypeModel");

            migrationBuilder.RenameTable(
                name: "ProjectTypeModel",
                newName: "ProjectTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTypeModel_PortfolioProfileId",
                table: "ProjectTypes",
                newName: "IX_ProjectTypes_PortfolioProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTypes",
                table: "ProjectTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConversationInfo",
                columns: table => new
                {
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversationPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationInfo", x => x.ConversationId);
                    table.ForeignKey(
                        name: "FK_ConversationInfo_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationInfo_Photos_ConversationPhotoId",
                        column: x => x.ConversationPhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConversationParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationParticipants_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConversationParticipants_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatAttachements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatAttachements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatAttachements_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReceipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReceipts_AspNetUsers_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageReceipts_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatAttachmentFilesModel",
                columns: table => new
                {
                    ChatAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatAttachmentFilesModel", x => new { x.ChatAttachmentId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ChatAttachmentFilesModel_ChatAttachements_ChatAttachmentId",
                        column: x => x.ChatAttachmentId,
                        principalTable: "ChatAttachements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatAttachmentFilesModel_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatAttachmentPhotoModel",
                columns: table => new
                {
                    ChatAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatAttachmentPhotoModel", x => new { x.ChatAttachmentId, x.PhotoModelId });
                    table.ForeignKey(
                        name: "FK_ChatAttachmentPhotoModel_ChatAttachements_ChatAttachmentId",
                        column: x => x.ChatAttachmentId,
                        principalTable: "ChatAttachements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatAttachmentPhotoModel_Photos_PhotoModelId",
                        column: x => x.PhotoModelId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatAttachmentVideoModel",
                columns: table => new
                {
                    ChatAttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatAttachmentVideoModel", x => new { x.ChatAttachmentId, x.VideoModelId });
                    table.ForeignKey(
                        name: "FK_ChatAttachmentVideoModel_ChatAttachements_ChatAttachmentId",
                        column: x => x.ChatAttachmentId,
                        principalTable: "ChatAttachements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatAttachmentVideoModel_Videos_VideoModelId",
                        column: x => x.VideoModelId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatAttachements_ChatMessageId",
                table: "ChatAttachements",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAttachmentFilesModel_FileId",
                table: "ChatAttachmentFilesModel",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAttachmentPhotoModel_PhotoModelId",
                table: "ChatAttachmentPhotoModel",
                column: "PhotoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAttachmentVideoModel_VideoModelId",
                table: "ChatAttachmentVideoModel",
                column: "VideoModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId",
                table: "ChatMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserModelId",
                table: "ChatMessages",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationInfo_ConversationPhotoId",
                table: "ConversationInfo",
                column: "ConversationPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationParticipants_ConversationId",
                table: "ConversationParticipants",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationParticipants_UserModelId",
                table: "ConversationParticipants",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserModelId",
                table: "Conversations",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceipts_ChatMessageId",
                table: "MessageReceipts",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceipts_UserModelId",
                table: "MessageReceipts",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectTypes_ProjectTypeId",
                table: "Projects",
                column: "ProjectTypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypes_PortfolioProfile_PortfolioProfileId",
                table: "ProjectTypes",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectTypes_ProjectTypeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypes_PortfolioProfile_PortfolioProfileId",
                table: "ProjectTypes");

            migrationBuilder.DropTable(
                name: "ChatAttachmentFilesModel");

            migrationBuilder.DropTable(
                name: "ChatAttachmentPhotoModel");

            migrationBuilder.DropTable(
                name: "ChatAttachmentVideoModel");

            migrationBuilder.DropTable(
                name: "ConversationInfo");

            migrationBuilder.DropTable(
                name: "ConversationParticipants");

            migrationBuilder.DropTable(
                name: "MessageReceipts");

            migrationBuilder.DropTable(
                name: "ChatAttachements");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTypes",
                table: "ProjectTypes");

            migrationBuilder.RenameTable(
                name: "ProjectTypes",
                newName: "ProjectTypeModel");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTypes_PortfolioProfileId",
                table: "ProjectTypeModel",
                newName: "IX_ProjectTypeModel_PortfolioProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTypeModel",
                table: "ProjectTypeModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectTypeModel_ProjectTypeId",
                table: "Projects",
                column: "ProjectTypeId",
                principalTable: "ProjectTypeModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypeModel_PortfolioProfile_PortfolioProfileId",
                table: "ProjectTypeModel",
                column: "PortfolioProfileId",
                principalTable: "PortfolioProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
