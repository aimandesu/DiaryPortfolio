using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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
                                    new Error(
                                        System.Net.HttpStatusCode.Unauthorized, 
                                        "User not authorized, please login")
                                ));
                            break;

                        case InvalidOperationException ex:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(
                                    new Error(System.Net.HttpStatusCode.BadRequest, ex.Message)
                                ));
                            break;

                        case AppException ex:
                            context.Response.StatusCode = (int)ex.StatusCode;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(
                                    new Error(ex.StatusCode, ex.Message)
                                ));
                            break;

                        case ValidationException ex:
                            var errors = ex.Errors
                                .Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                                .ToList();

                            var error = new Error(
                                HttpStatusCode.UnprocessableEntity,
                                "Validation failed",
                                errors
                            );

                            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(error));
                            break;

                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsJsonAsync(
                                ResultResponse<object>.Failure(
                                    new Error(
                                        System.Net.HttpStatusCode.InternalServerError, 
                                        exception?.Message ?? "")
                                ));
                            break;
                    }
                });
            });
        }
    }
}
