using Microsoft.AspNetCore.DataProtection;

namespace DiaryPortfolio.Api.Extensions
{
    public static class ConfigureDataProtectionExtension
    {
        public static void ConfigureDataProtection(this IServiceCollection services)
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"D:\Sites\site64986\keys"))
                .SetApplicationName("DiaryPortfolio");
        }
    }
}
