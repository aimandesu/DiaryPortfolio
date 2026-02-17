using Microsoft.Extensions.FileProviders;

namespace DiaryPortfolio.Api.Extensions
{
    public static class ConfigureMediaExtensions
    {
        public static void ConfigureMedia(this WebApplication web)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            web.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/uploads"
            });
        }
    }
}
