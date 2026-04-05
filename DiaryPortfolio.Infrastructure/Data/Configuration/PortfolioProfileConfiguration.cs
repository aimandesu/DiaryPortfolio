using DiaryPortfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DiaryPortfolio.Infrastructure.Data.Configuration
{
    internal class PortfolioProfileConfiguration : IEntityTypeConfiguration<PortfolioProfile>
    {
        public void Configure(EntityTypeBuilder<PortfolioProfile> builder)
        {
            // DiaryProfileConfiguration
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.User)
                .WithOne(u => u.PortfolioProfile)
                .HasForeignKey<PortfolioProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // inside PortfolioProfileConfiguration
            builder.HasOne(p => p.ProfilePhoto)
                .WithOne()
                .HasForeignKey<PortfolioProfile>(p => p.ProfilePhotoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Resume)
                .WithOne()
                .HasForeignKey<PortfolioProfile>(p => p.ResumeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Location)
                .WithOne()
                .HasForeignKey<PortfolioProfile>(p => p.LocationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
