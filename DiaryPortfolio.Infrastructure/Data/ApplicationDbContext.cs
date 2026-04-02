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
    public DbSet<PhotoModel> Photos { get; set; }
    public DbSet<VideoModel> Videos { get; set; }
    public DbSet<TextModel> TextStyle { get; set; }
    //latest
    public DbSet<ResumeModel> Resume { get; set; }
    public DbSet<ResumeTemplateModel> ResumeTemplate { get; set; }
    public DbSet<CustomUrl> CustomUrls { get; set; }
    public DbSet<ExperienceModel> Experiences { get; set; }
    public DbSet<SkillModel> Skills { get; set; }
    public DbSet<EducationModel> Educations { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<FilesModel> Files { get; set; }
    public DbSet<TypeModel> Types { get; set; }
    public DbSet<SelectionModel> Selections { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SpaceModel>()
           .HasIndex(s => s.Title)
           .IsUnique();


        modelBuilder.Entity<MediaModel>(builder =>
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

            builder
                .HasOne(m => m.LocationModel);
                //.WithOne(c => c.MediaModel)
                //.HasForeignKey<LocationModel>(m => m.MediaId);

            builder
                .HasOne(m => m.TextModel)
                .WithMany()
                .HasForeignKey(m => m.TextId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(m => m.TextId)
                 .IsUnique(false);

            //Conversion to string for Enum properties
            //builder.Property(m => m.MediaStatus)
            //    .HasConversion<string>();

            //builder.Property(m => m.MediaType)
            //    .HasConversion<string>();

            //somehow these two to make when reading the data, it able to automatically convert it
            //builder
            //    .HasOne(m => m.SelectionMediaStatusModel)
            //    .WithMany()
            //    .HasForeignKey(m => m.MediaStatusSelectionId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasOne(m => m.SelectionMediaTypeModel)
            //    .WithMany()
            //    .HasForeignKey(m => m.MediaTypeSelectionId)
            //    .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PhotoModel>(builder =>
        {
            builder
                .HasOne(p => p.MediaModel)
                .WithMany(m => m.PhotoModels)
                .HasForeignKey(p => p.MediaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<VideoModel>(builder =>
        {
            builder
                .HasOne(v => v.MediaModel)
                .WithMany(m => m.VideoModels)
                .HasForeignKey(v => v.MediaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TextModel>()
            .Property(t => t.TextStyle)
            .HasConversion<string>();

    }

}
