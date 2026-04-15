using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Chat.Delete
{
    public sealed record class DeleteChatRequest(
        BroadcastModel BroadcastModel
    ) : IRequest<ResultResponse<object>>;
}
