using DiaryPortfolio.Application.Features.User.Chat.Create;
using DiaryPortfolio.Application.Features.User.Chat.Delete;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

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

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync(
                "ReceiveMessage", "System", $"{Context?.User?.Identity?.Name} joined {roomName}");
        }

        public async Task SendMessage(CreateChatRequest request)
        {
            await _mediator.Send(
                request,
                CancellationToken.None
            );
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
