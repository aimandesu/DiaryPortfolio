using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverBiDi.Protocol;

namespace DiaryPortfolio.Infrastructure.Services
{
    public class ChatNotifier : IChatNotifier
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatNotifier(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastMessageAll(BroadcastModel broadcastModel)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", broadcastModel);
        }

        public async Task SendMessageToConversation(BroadcastModel broadcastModel)
        {
            if (!string.IsNullOrEmpty(broadcastModel.ConversationId))
            {
                // GROUP MESSAGE: Everyone in this room will see this
                await _hubContext.Clients.Group(broadcastModel.ConversationId)
                    .SendAsync("ReceiveMessage", broadcastModel);
            }
        }

        public async Task SendUserNotification(BroadcastModel broadcastModel)
        {
            if (!string.IsNullOrEmpty(broadcastModel.UserId))
            {
                /* 
                 * This is actually for like user event, say like 
                 * 1. "You have a new message"
                 * 2. "User X added you"
                 * 3. "Your account was logged in from new device"
                 */
                await _hubContext.Clients.User(broadcastModel.UserId) 
                    .SendAsync("ReceiveMessage", broadcastModel);
            }
        }

        public async Task DeleteMessage(BroadcastModel broadcastModel)
        {
            await _hubContext.Clients.All.SendAsync("MessageDeleted", broadcastModel);
        }
    }
}
