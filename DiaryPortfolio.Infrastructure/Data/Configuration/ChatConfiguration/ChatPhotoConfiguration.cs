using DiaryPortfolio.Domain.Entities.Chat.Attachment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Data.Configuration.ChatConfiguration
{
    internal class ChatPhotoConfiguration : IEntityTypeConfiguration<ChatAttachmentPhotoModel>
    {
        public void Configure(
            EntityTypeBuilder<ChatAttachmentPhotoModel> builder)
        {
            builder
                .HasKey(cap => new { cap.ChatAttachmentId, cap.PhotoModelId });
        }
    }
}
