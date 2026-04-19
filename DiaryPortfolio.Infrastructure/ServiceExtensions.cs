using DiaryPortfolio.Application.Features.User.Chat.Create;
using DiaryPortfolio.Application.Helpers.Authentication;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Repository;
using DiaryPortfolio.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiaryPortfolio.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                //.EnableSensitiveDataLogging()
            );
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IMediaHandlerRepository, MediaHandlerRepository>();
        services.AddScoped<IFileHandlerRepository, FileHandlerRepository>();
        services.AddScoped<IFilePathHandlerRepository, FilePathHandlerRepository>();
        services.AddScoped<ISpaceRepository, SpaceRepository>();
        services.AddScoped<IPortfolioProfileRepository, PortfolioProfileRepository>();
        services.AddScoped<IDiaryProfileRepository, DiaryProfileRepository>();
        services.AddScoped<IResumeRepository, ResumeRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ICustomUrlRepository, CustomUrlRepository>();
        //services.AddScoped<IExperienceRepository, ExperienceRepository>();

        //this is for the top one -> the one we do addscoped IExperienceRepository with ExperienceRepository
        //-> to use this, you need to make the repository a public repository, if you try to extend with
        //IBaseRepository
        services.Scan(scan => scan
            .FromAssemblies(typeof(BaseRepository<>).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        //Helper
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthenticationRepository, AuthService>();
        services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
        services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
        services.AddScoped<ISelectionHelper, SelectionHelper>();

        //Signal R
        services.AddSignalR(
            options => options.EnableDetailedErrors = true
        );

        services.AddScoped<IChatNotifier, ChatNotifier>();
    }
}