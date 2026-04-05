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
    internal class DiaryProfileConfiguration : IEntityTypeConfiguration<DiaryProfile>
    {
        public void Configure(EntityTypeBuilder<DiaryProfile> builder)
        {
            // DiaryProfileConfiguration
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.User)
                   .WithOne(u => u.DiaryProfile)
                   .HasForeignKey<DiaryProfile>(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
