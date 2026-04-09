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
                .EnableSensitiveDataLogging()
            );
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IMediaHandlerRepository, MediaHandlerRepository>();
        services.AddScoped<IFileHandlerRepository, FileHandlerRepository>();
        services.AddScoped<IFilePathHandlerRepository, FilePathHandlerRepository>();
        services.AddScoped<ISpaceRepository, SpaceRepository>();

        //Helper
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthenticationRepository, AuthService>();
    }
}