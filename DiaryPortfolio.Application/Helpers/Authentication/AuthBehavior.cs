using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers.Authentication
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public AuthBehavior(
            IUserService userService,
            IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        public async ValueTask<TResponse> Handle(
            TRequest request, 
            MessageHandlerDelegate<TRequest, TResponse> next, 
            CancellationToken cancellationToken)
        {
            if (request is IRequireAuthentication)
            {
                if (!_userService.IsAuthenticated || _userService.UserId is null)
                    throw new UnauthorizedAccessException();

                var profileType = request switch
                {
                    IRequirePortfolioProfile => ProfileType.Portfolio,
                    IRequireDiaryProfile => ProfileType.Diary,
                    _ => throw new InvalidOperationException(
                             $"Request '{typeof(TRequest).Name}' requires authentication but has no profile type given.")
                };

                var user = await _userRepository.GetUserByUserId(
                    _userService.UserId ?? Guid.Empty, 
                    profileType);

                if (user == null)
                {
                    throw new AppException(
                        $"User does not have {profileType} account yet",
                        System.Net.HttpStatusCode.Unauthorized
                    );
                }

                var ownershipAttr = typeof(TRequest)
                    .GetCustomAttribute<RequireOwnershipAttribute>();


                if (ownershipAttr != null)
                {
                    var resourceIdProp = typeof(TRequest).GetProperty("Id"); //this value must follow controller so for all use Id

                    Guid? resourceId = ownershipAttr.ResourceType switch 
                    //this scenario might not even happen tbh, because we already
                    //encapsulate area needed portfolioprofileid with their own userservice id
                    {
                        var t when t == typeof(PortfolioProfileModel) => _userService.PortfolioProfileId,
                        var t when t == typeof(DiaryProfileModel) => _userService.DiaryProfileId,
                        _ => Guid.TryParse(resourceIdProp!.GetValue(request)?.ToString(), out var parsed)
                                                            ? parsed
                                                            : null
                    };

                    await _userRepository.EnsureOwnerAsync(
                        resourceId ?? Guid.Empty,
                        ownershipAttr.ResourceType);
                }


            }

            return await next(request, cancellationToken);
        }
    }
}
