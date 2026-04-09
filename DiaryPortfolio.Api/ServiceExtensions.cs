using DiaryPortfolio.Application.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace DiaryPortfolio.Api
{
    public static class ServiceExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features
                        .Get<IExceptionHandlerFeature>()?
                        .Error;

                    context.Response.ContentType = "application/json";

                    switch (exception)
                    {
                        case UnauthorizedAccessException:
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(
                                    new Error(System.Net.HttpStatusCode.Unauthorized, "Please login")
                                ));
                            break;

                        case InvalidOperationException ex:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(
                                    new Error(System.Net.HttpStatusCode.BadRequest, ex.Message)
                                ));
                            break;

                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(
                                    new Error(System.Net.HttpStatusCode.InternalServerError, exception?.Message ?? "")
                                ));
                            break;
                    }
                });
            });
        }
    }
}
