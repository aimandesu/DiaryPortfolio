using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Services
{
    internal class ChatGroupService : IChatGroupService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatGroupService(
            IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task JoinConversation(
            string connectionId,
            Guid roomId)
        {
            string roomName = roomId.ToString(); //need to pull from db
            string userName = string.Empty; //pull from icontext

            await _hubContext.Groups.AddToGroupAsync(connectionId, roomName);

            await _hubContext.Clients.Group(roomName).SendAsync(
                "ReceiveMessage",
                "System",
                $"{userName} joined {roomName}"
            );
        }

        public Task LeaveConversation(
            string connectionId, 
            Guid roomId)
        {
            throw new NotImplementedException();
        }
    }
}
