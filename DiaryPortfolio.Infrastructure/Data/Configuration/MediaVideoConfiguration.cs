using DiaryPortfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Data.Configuration
{
    internal class MediaVideoConfiguration : IEntityTypeConfiguration<MediaVideoModel>
    {
        public void Configure(EntityTypeBuilder<MediaVideoModel> builder)
        {
            builder.HasKey(mv => new { mv.MediaModelId, mv.VideoModelId });

            builder.HasOne(mv => mv.Media)
                .WithMany(m => m.MediaVideos)
                .HasForeignKey(mv => mv.MediaModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mv => mv.Video)
                .WithMany()
                .HasForeignKey(mv => mv.VideoModelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
