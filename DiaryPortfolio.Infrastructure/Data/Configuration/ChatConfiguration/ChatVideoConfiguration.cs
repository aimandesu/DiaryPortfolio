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
    internal class ChatVideoConfiguration : IEntityTypeConfiguration<ChatAttachmentVideoModel>
    {
        public void Configure(
            EntityTypeBuilder<ChatAttachmentVideoModel> builder)
        {
            builder
                .HasKey(cav => new { cav.ChatAttachmentId, cav.VideoModelId });
        }
    }
}
