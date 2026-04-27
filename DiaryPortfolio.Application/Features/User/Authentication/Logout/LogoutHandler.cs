using DiaryPortfolio.Application.IRepository;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.Logout
{
    internal class LogoutHandler : IRequestHandler<LogoutRequest>
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public LogoutHandler(
            IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public async ValueTask<Unit> Handle(
            LogoutRequest request, 
            CancellationToken cancellationToken)
        {
            await _authenticationRepository.Logout();

            return Unit.Value;
        }
    }
}
