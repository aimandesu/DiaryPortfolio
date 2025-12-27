using DiaryPortfolio.Application.IServices;
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

        public AuthBehavior(IUserService userService)
        {
            _userService = userService;
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
            }

            return await next(request, cancellationToken);
        }
    }
}
