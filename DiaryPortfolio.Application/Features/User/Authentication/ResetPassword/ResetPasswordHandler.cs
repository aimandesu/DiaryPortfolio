using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using Mediator;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.ResetPassword
{
    internal class ResetPasswordHandler(
        IAuthenticationRepository authenticationRepository)
        : IRequestHandler<ResetPasswordRequest, ResultResponse<bool>>
    {
        public async ValueTask<ResultResponse<bool>> Handle(
            ResetPasswordRequest request,
            CancellationToken cancellationToken)
        {
            var user = await authenticationRepository.FindByIdAsync(request.UserId);

            if (user is null)
            {
                return ResultResponse<bool>.Failure(
                    new Error(HttpStatusCode.BadRequest, "Invalid or expired reset link"));
            }

            var result = await authenticationRepository.ResetPasswordAsync(
                user, request.Token, request.NewPassword);

            if (!result.Succeeded)
            {
                var description = string.Join(" ", result.Errors.Select(e => e.Description));
                return ResultResponse<bool>.Failure(
                    new Error(HttpStatusCode.BadRequest,
                        string.IsNullOrWhiteSpace(description) ? "Invalid or expired reset link" : description));
            }

            return ResultResponse<bool>.Success(true);
        }
    }
}
