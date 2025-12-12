using DiaryPortfolio.Application.IRepository.IUserRepository;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryPortfolio.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUserRepository, UserRepository>();
    }
}