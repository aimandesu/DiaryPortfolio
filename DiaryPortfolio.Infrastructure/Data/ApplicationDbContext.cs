using DiaryPortfolio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiaryPortfolio.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>(options)
{
    //Tables
    public override DbSet<UserModel> Users { get; set; }
    public DbSet<SpaceModel> Spaces { get; set; }
    public DbSet<MediaModel> Medias { get; set; }
    public DbSet<CollectionModel> Collections { get; set; }
    public DbSet<ConditionModel> Conditions { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<VideoModel> Videos { get; set; }
    public DbSet<PhotoModel> Photos { get; set; }
    public DbSet<TextModel> TextStyle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CollectionModel>(builder =>
        {
            builder
                .HasMany(c => c.MediaModels)
                .WithOne(m => m.CollectionModel)
                .HasForeignKey(m => m.CollectionId);
        });

        modelBuilder.Entity<MediaModel>(builder =>
        {
            builder
                .HasOne(m => m.ConditionModel)
                .WithOne(c => c.MediaModel)
                .HasForeignKey<ConditionModel>(m => m.MediaId);

            builder
                .HasOne(m => m.LocationModel)
                .WithOne(c => c.MediaModel)
                .HasForeignKey<LocationModel>(m => m.MediaId);

        });

    }

}
