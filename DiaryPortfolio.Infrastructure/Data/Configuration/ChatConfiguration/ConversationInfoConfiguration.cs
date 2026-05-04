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
    internal class ConversationInfoConfiguration : IEntityTypeConfiguration<ConversationInfoModel>
    {
        public void Configure(
            EntityTypeBuilder<ConversationInfoModel> builder)
        {
            builder
                .HasKey(x => x.ConversationId);
        }
    }
}
