using DiaryPortfolio.Application.Helpers.Authentication;
using DiaryPortfolio.Application.Helpers.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {

            services.AddMediator(options =>
            {
                options.ServiceLifetime = ServiceLifetime.Scoped;
                options.PipelineBehaviors = [
                    typeof(AuthBehavior<,>),
                    typeof(LoggingBehavior<,>)
                ];
            });

        }
    }
}