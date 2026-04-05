using DiaryPortfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Data.Configuration
{
    internal class SpaceConfiguration : IEntityTypeConfiguration<SpaceModel>
    {
        public void Configure(EntityTypeBuilder<SpaceModel> builder)
        {
            builder
                .HasIndex(s => new { s.DiaryProfileId, s.Title })
                .IsUnique();
        }
    }
}
