using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using Mediator;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.ConfirmEmail
{
    internal class ConfirmEmailHandler(
        IAuthenticationRepository authenticationRepository)
        : IRequestHandler<ConfirmEmailRequest, ResultResponse<bool>>
    {
        public async ValueTask<ResultResponse<bool>> Handle(
            ConfirmEmailRequest request,
            CancellationToken cancellationToken)
        {
            var user = await authenticationRepository.FindByIdAsync(request.UserId);

            if (user is null)
            {
                return ResultResponse<bool>.Failure(
                    new Error(HttpStatusCode.BadRequest, "Invalid or expired confirmation link"));
            }

            var result = await authenticationRepository.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
            {
                return ResultResponse<bool>.Failure(
                    new Error(HttpStatusCode.BadRequest, "Invalid or expired confirmation link"));
            }

            return ResultResponse<bool>.Success(true);
        }
    }
}
