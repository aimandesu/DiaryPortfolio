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
    internal class MediaPhotoConfiguration : IEntityTypeConfiguration<MediaPhotoModel>
    {
        public void Configure(EntityTypeBuilder<MediaPhotoModel> builder)
        {
            builder.HasKey(mp => new { mp.MediaModelId, mp.PhotoModelId });

            builder.HasOne(mp => mp.Media)
                .WithMany(m => m.MediaPhotos)
                .HasForeignKey(mp => mp.MediaModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mp => mp.Photo)
                .WithMany()
                .HasForeignKey(mp => mp.PhotoModelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
