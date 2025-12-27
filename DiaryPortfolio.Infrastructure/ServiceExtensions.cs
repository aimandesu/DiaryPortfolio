using DiaryPortfolio.Application.IRepository.IFileHandlerRepository;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Application.IRepository.IMediaRepository;
using DiaryPortfolio.Application.IRepository.ISpaceRepository;
using DiaryPortfolio.Application.IRepository.ITokenRepository;
using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Repository.FileHandler;
using DiaryPortfolio.Infrastructure.Repository.Media;
using DiaryPortfolio.Infrastructure.Repository.MediaHandler;
using DiaryPortfolio.Infrastructure.Repository.Space;
using DiaryPortfolio.Infrastructure.Repository.Token;
using DiaryPortfolio.Infrastructure.Repository.User;
using DiaryPortfolio.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryPortfolio.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(
                connectionString
                )
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
    }
}