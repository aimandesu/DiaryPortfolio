using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Chat.Delete
{
    internal class DeleteChatHandler : IRequestHandler<DeleteChatRequest, ResultResponse<object>>
    {
        public readonly IChatNotifier _chatNotifier;
        public DeleteChatHandler(
            IChatNotifier chatNotifier)
        {
            _chatNotifier = chatNotifier;
        }
        public async ValueTask<ResultResponse<object>> Handle(
            DeleteChatRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                await _chatNotifier.DeleteMessage(request.BroadcastModel);
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
