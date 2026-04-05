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
    internal class PhotoConfiguration : IEntityTypeConfiguration<PhotoModel>
    {
        public void Configure(EntityTypeBuilder<PhotoModel> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
