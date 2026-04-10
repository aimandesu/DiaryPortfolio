using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
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

                var user = await _userRepository.GetUserByUsername(
                    _userService.UserName ?? "", 
                    profileType);

                if (user == null)
                {
                    throw new AppException(
                        $"User does not have {profileType} account yet",
                        System.Net.HttpStatusCode.Unauthorized
                    );
                }


            }

            return await next(request, cancellationToken);
        }
    }
}
