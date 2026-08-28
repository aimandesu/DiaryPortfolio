using DiaryPortfolio.Application.Features.User.Chat.Create;
using DiaryPortfolio.Application.Features.User.Chat.Delete;
using DiaryPortfolio.Application.Features.User.ChatGroup.Join;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiaryPortfolio.Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        //private readonly CreateChatHandler _createChatHandler;
        private readonly IMediator _mediator;

        public ChatHub(
            //CreateChatHandler createChatHandler
            IMediator mediator
            )
        {
            //_createChatHandler = createChatHandler;
            _mediator = mediator;
        }

        public async Task JoinConversation(string conversationId)
        {
            await _mediator.Send(
               request: new JoinChatGroupRequest
               (
                   ConnectionId: Context.ConnectionId,
                   ConversationId: conversationId
               ),
               CancellationToken.None
            );
        }

        public async Task<string> SendMessage(string jsonMessageUploadRequest)
        {

            var messageUploadRequest = JsonSerializer.Deserialize<MessageUpload>(
                jsonMessageUploadRequest,
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNameCaseInsensitive = true
                });

            var request = new CreateChatRequest(messageUploadRequest);

            var result = await _mediator.Send(
                request,
                CancellationToken.None
            );

            return result.Result?.ToString() ?? "";
        }

        public async Task DeleteMessage(DeleteChatRequest request)
        {
            await _mediator.Send(
                request,
                CancellationToken.None
            );
        }

        public async Task UserTyping(string roomId)
        {
            await Clients.OthersInGroup(roomId).SendAsync("UserIsTyping", Context?.User?.Identity?.Name);
        }

    }
}
