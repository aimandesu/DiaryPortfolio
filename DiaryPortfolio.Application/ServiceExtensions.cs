using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.PortfolioProfile.Project.Create;
using DiaryPortfolio.Application.Helpers.Authentication;
using DiaryPortfolio.Application.Helpers.Logger;
using DiaryPortfolio.Application.Helpers.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(
                Assembly.GetExecutingAssembly()
                //includeInternalTypes: true -> use this if your validation is internal class
            );

            services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
                options.PipelineBehaviors = [
                    typeof(AuthBehavior<,>),
                    typeof(LoggingBehavior<,>),
                    typeof(ValidationBehavior<,>)
                ];
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState
                    );

                    var errors = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    return new BadRequestObjectResult(
                        ResultResponse<object>.Failure(
                            new Error(
                                HttpStatusCode.BadRequest,
                                 problemDetails.Title ?? "One or more validation errors occurred.",
                                errors
                            )
                        )
                    );
                };
            });

        }
    }
}