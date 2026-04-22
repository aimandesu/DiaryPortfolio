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
    internal class ResumeConfiguration : IEntityTypeConfiguration<ResumeModel>
    {
        public void Configure(EntityTypeBuilder<ResumeModel> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.ResumeFile)
                .WithMany()
                .HasForeignKey(r => r.FileId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.ResumeTemplate)
                .WithMany()
                .HasForeignKey(r => r.TemplateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.PortfolioProfile)
                .WithOne(p => p.Resume)
                .HasForeignKey<ResumeModel>(r => r.PortfolioProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
