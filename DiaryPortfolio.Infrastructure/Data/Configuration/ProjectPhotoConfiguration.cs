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
    internal class ProjectPhotoConfiguration : IEntityTypeConfiguration<ProjectPhotoModel>
    {
        public void Configure(EntityTypeBuilder<ProjectPhotoModel> builder)
        {
            builder.HasKey(pp => new { pp.ProjectModelId, pp.PhotoModelId });

            builder.HasOne(pp => pp.Project)
                .WithMany(p => p.ProjectPhotos)
                .HasForeignKey(pp => pp.ProjectModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pp => pp.Photo)
                .WithMany()
                .HasForeignKey(pp => pp.PhotoModelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
