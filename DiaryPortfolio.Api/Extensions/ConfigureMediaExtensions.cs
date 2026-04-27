using Microsoft.Extensions.FileProviders;

namespace DiaryPortfolio.Api.Extensions
{
    public static class ConfigureMediaExtensions
    {
        public static void ConfigureMedia(this WebApplication app)
        {
            var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/uploads"
            });
        }
    }
}
