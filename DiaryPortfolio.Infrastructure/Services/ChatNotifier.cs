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

        public async Task BroadcastMessageGroup(BroadcastModel broadcastModel)
        {
            if (!string.IsNullOrEmpty(broadcastModel.GroupId))
            {
                // GROUP MESSAGE: Everyone in this room will see this
                await _hubContext.Clients.Group(broadcastModel.GroupId)
                    .SendAsync("ReceiveMessage", broadcastModel);
            }
        }

        public async Task BroadcastMessagePrivate(BroadcastModel broadcastModel)
        {
            if (!string.IsNullOrEmpty(broadcastModel.UserId))
            {
                // PRIVATE MESSAGE: Only TargetUserId will see this
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
