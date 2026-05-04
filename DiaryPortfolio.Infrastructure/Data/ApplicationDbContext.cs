using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Entities.Chat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiaryPortfolio.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>(options)
{
    //Diary Profile
    public DbSet<SpaceModel> Spaces { get; set; }
    public DbSet<MediaModel> Medias { get; set; }
    public DbSet<CollectionModel> Collections { get; set; }
    public DbSet<ConditionModel> Conditions { get; set; }

    //Shared
    public DbSet<PhotoModel> Photos { get; set; }
    public DbSet<VideoModel> Videos { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<FileModel> Files { get; set; }
    public DbSet<TypeModel> Types { get; set; }
    public DbSet<SelectionModel> Selections { get; set; }

    //Shared - Chat Model
    public DbSet<ConversationModel> Conversations { get; set; }
    public DbSet<ConversationInfoModel> ConversationInfo { get; set; }
    public DbSet<ConversationParticipantModel> ConversationParticipants { get; set; }
    public DbSet<ChatMessageModel> ChatMessages { get; set; }
    public DbSet<MessageReceiptModel> MessageReceipts { get; set; }
    public DbSet<ChatAttachementModel> ChatAttachements { get; set; }

    //Portfolio Profile
    public DbSet<ResumeModel> Resume { get; set; }
    public DbSet<ResumeTemplateModel> ResumeTemplate { get; set; }
    public DbSet<CustomUrlModel> CustomUrls { get; set; }
    public DbSet<ExperienceModel> Experiences { get; set; }
    public DbSet<SkillModel> Skills { get; set; }
    public DbSet<EducationModel> Educations { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<ProjectTypeModel> ProjectTypes { get; set; }

    //Locations
    public DbSet<PostalCodeModel> PostalCodes { get; set; }
    public DbSet<CityModel> Cities { get; set; }
    public DbSet<StateModel> States { get; set; }

    //main path
    public override DbSet<UserModel> Users { get; set; }
    public DbSet<DiaryProfileModel> DiaryProfile { get; set; }
    public DbSet<PortfolioProfileModel> PortfolioProfile { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }

}
