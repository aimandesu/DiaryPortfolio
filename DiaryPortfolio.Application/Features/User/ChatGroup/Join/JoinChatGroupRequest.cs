using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.ChatGroup.Join
{
    public sealed record class JoinChatGroupRequest(
        string ConnectionId,
        string ConversationId) : IRequest;
}
