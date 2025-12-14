using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace DiaryPortfolio.Api.Extensions
{
    public static class ConfigureIdentityExtensions
    {
        public static void ConfigureIdentityPolicy(this IServiceCollection services)
        {
            // AddIdentityCore only has a single generic parameter.
            // If you need roles, add them explicitly and use IdentityRole<Guid>
            // because your UserModel uses IdentityUser<Guid>.
            services
                .AddIdentityCore<UserModel>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                })
                .AddRoles<IdentityRole<Guid>>() // ensure role key type matches user key type (Guid)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints(); 
        }
    }
}
