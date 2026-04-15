using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Chat.Create
{
    public class CreateChatHandler : IRequestHandler<CreateChatRequest, ResultResponse<object>>
    {
        private readonly IChatNotifier _chatNotifier;
        public CreateChatHandler(
            IChatNotifier chatNotifier)
        {
            _chatNotifier = chatNotifier;
        }

        public async ValueTask<ResultResponse<object>> Handle(
            CreateChatRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                //change to enum later, switch case

                if (!string.IsNullOrEmpty(request.BroadcastModel.UserId))
                {
                    await _chatNotifier.BroadcastMessagePrivate(request.BroadcastModel);
                }
                else if (!string.IsNullOrEmpty(request.BroadcastModel.GroupId))
                {
                    await _chatNotifier.BroadcastMessageGroup(request.BroadcastModel);
                }
                else
                {
                    await _chatNotifier.BroadcastMessageAll(request.BroadcastModel);
                }

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
