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
    internal class ProjectVideoConfiguration : IEntityTypeConfiguration<ProjectVideoModel>
    {
        public void Configure(EntityTypeBuilder<ProjectVideoModel> builder)
        {
            builder.HasKey(mv => new { mv.ProjectModelId, mv.VideoModelId });

            builder.HasOne(mv => mv.Project)
                .WithMany(m => m.ProjectVideos)
                .HasForeignKey(mv => mv.ProjectModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mv => mv.Video)
                .WithMany()
                .HasForeignKey(mv => mv.VideoModelId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
