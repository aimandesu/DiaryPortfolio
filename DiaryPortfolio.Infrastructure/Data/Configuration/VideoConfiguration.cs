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
    internal class VideoConfiguration : IEntityTypeConfiguration<VideoModel>
    {
        public void Configure(EntityTypeBuilder<VideoModel> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
