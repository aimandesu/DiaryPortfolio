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
    internal class FileConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.Selection)
                .WithMany()
                .HasForeignKey(f => f.SelectionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
