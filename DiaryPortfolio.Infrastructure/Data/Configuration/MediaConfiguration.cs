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
    internal class MediaConfiguration : IEntityTypeConfiguration<MediaModel>
    {
        public void Configure(EntityTypeBuilder<MediaModel> builder)
        {
            builder.HasOne(m => m.CollectionModel)
                .WithMany(c => c.MediaModels)
                .HasForeignKey(m => m.CollectionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.SpaceModel)
                .WithMany(s => s.MediaModels)
                .HasForeignKey(m => m.SpaceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.ConditionModel)
                .WithOne(c => c.MediaModel)
                .HasForeignKey<ConditionModel>(m => m.MediaId);

            builder.HasOne(m => m.LocationModel)
                .WithOne()
                .HasForeignKey<MediaModel>(m => m.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
            //.WithOne(c => c.MediaModel)
            //.HasForeignKey<LocationModel>(m => m.MediaId);

            //Conversion to string for Enum properties
            //builder.Property(m => m.MediaStatus)
            //    .HasConversion<string>();

            //builder.Property(m => m.MediaType)
            //    .HasConversion<string>();

            builder
                .HasOne(m => m.SelectionMediaStatusModel)
                .WithMany()
                .HasForeignKey(m => m.MediaStatusSelectionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.SelectionMediaTypeModel)
                .WithMany()
                .HasForeignKey(m => m.MediaTypeSelectionId)
                .OnDelete(DeleteBehavior.Restrict);

            //property
            builder
                .Property(m => m.MediaStatusSelectionId)
                .IsRequired();

            builder
                .Property(m => m.MediaTypeSelectionId)
                .IsRequired();
        }
    }
}
