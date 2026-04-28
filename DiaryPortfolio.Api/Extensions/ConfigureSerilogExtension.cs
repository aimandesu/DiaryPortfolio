using Serilog;
using Serilog.Extensions.Hosting;

namespace DiaryPortfolio.Api.Extensions
{
    public static class ConfigureSerilogExtension
    {
        public static void ConfigureSerilog(this ConfigureHostBuilder host)
        {

            host.UseSerilog((ctx, config) =>
            {
                config.ReadFrom.Configuration(ctx.Configuration);
                config.WriteTo.Console();

                var logPath = Path.Combine(
                    ctx.HostingEnvironment.ContentRootPath, "logs", 
                    "diaryportfolio-.log");

                config.WriteTo.File(
                    path: logPath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30
                );

            });
        }
    }
}
