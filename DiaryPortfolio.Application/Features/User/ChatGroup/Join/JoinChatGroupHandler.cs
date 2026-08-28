using DiaryPortfolio.Application.IServices;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.ChatGroup.Join
{
    internal class JoinChatGroupHandler : IRequestHandler<JoinChatGroupRequest>
    {
        private readonly IChatGroupService _chatGroupService;

        public JoinChatGroupHandler(
            IChatGroupService chatGroupService)
        {
            _chatGroupService = chatGroupService;
        }

        public async ValueTask<Unit> Handle(
            JoinChatGroupRequest request, 
            CancellationToken cancellationToken)
        {
            await _chatGroupService.JoinConversation(
                    request.ConnectionId,
                    new Guid(request.ConversationId)
                );

            return Unit.Value;

        }
    }
}
