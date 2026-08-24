using Microsoft.AspNetCore.DataProtection;

namespace DiaryPortfolio.Api.Extensions
{
    public static class ConfigureDataProtectionExtension
    {
        public static void ConfigureDataProtection(this IServiceCollection services, IWebHostEnvironment environment)
        {
            DirectoryInfo keysDirectory;

            if (environment.IsDevelopment())
            {
                string localKeysPath = Path.Combine(System.AppContext.BaseDirectory, "local-keys");
                keysDirectory = new DirectoryInfo(localKeysPath);
            }
            else
            {
                // Production Deployment (Windows Server)
                keysDirectory = new DirectoryInfo(@"D:\Sites\site64986\keys");
            }

            services.AddDataProtection()
                .PersistKeysToFileSystem(keysDirectory)
                .SetApplicationName("DiaryPortfolio");
        }
    }
}
