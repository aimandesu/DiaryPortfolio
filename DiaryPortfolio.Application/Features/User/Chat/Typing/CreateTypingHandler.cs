using DiaryPortfolio.Application.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Chat.Typing
{
    internal class CreateTypingHandler : IRequestHandler<CreateTypingRequest, ResultResponse<object>>
    {
        public async ValueTask<ResultResponse<object>> Handle(
            CreateTypingRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                return ResultResponse<object>.Success(null);
            }
            catch (Exception ex)
            {
                return ResultResponse<object>.Failure(
                    new Error(HttpStatusCode.Conflict, ex.Message));
            }

        }
    }
}
