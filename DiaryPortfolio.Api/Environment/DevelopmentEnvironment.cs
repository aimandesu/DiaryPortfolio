using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiaryPortfolio.Api.Environment
{
    public static class DevelopmentEnvironment
    {

        public static void Action(this WebApplication app)
        {
            EnsureSeed(app);
        }
        public static void EnsureSeed(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment()) return;

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
            DatabaseSeed.Seed(context);
        }
    }
}