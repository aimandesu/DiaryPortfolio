namespace DiaryPortfolio.Api.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context, 
            RequestDelegate next)
        {
            _logger.LogInformation(">> {Method} {Path}",
           context.Request.Method,
           context.Request.Path);

            await next(context);

            var level = context.Response.StatusCode >= 400
                ? LogLevel.Warning
                : LogLevel.Information;

            _logger.Log(level, "<< {StatusCode} {Method} {Path}",
                context.Response.StatusCode,
                context.Request.Method,
                context.Request.Path);
        }
    }
}
