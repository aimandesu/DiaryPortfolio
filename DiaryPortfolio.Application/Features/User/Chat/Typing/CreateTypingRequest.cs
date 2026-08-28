using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Chat.Typing
{
    public sealed record class CreateTypingRequest(
        BroadcastModel BroadcastModel
    ) : IRequest<ResultResponse<object>>;
}
