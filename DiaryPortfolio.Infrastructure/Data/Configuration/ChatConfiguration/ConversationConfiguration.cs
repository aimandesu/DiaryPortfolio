using DiaryPortfolio.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Data.Configuration.ChatConfiguration
{
    internal class ConversationConfiguration : IEntityTypeConfiguration<ConversationModel>
    {
        public void Configure(
            EntityTypeBuilder<ConversationModel> builder)
        {
            builder
                .HasOne(c => c.ConversationInfo)
                .WithOne(i => i.Conversation)
                .HasForeignKey<ConversationInfoModel>(i => i.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
