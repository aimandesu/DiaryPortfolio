using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Entities.Chat;
using DiaryPortfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class ChatMessageRepository : IChatMessageRepository
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public ChatMessageRepository(
            IUserService userService,
            ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<ResultResponse<ChatMessageModel>> CreateMessage(
            MessageUpload messageUpload,
            Guid ConversationId)
        {
            try
            {
                var message = new ChatMessageModel
                {
                    Content = messageUpload.Message,
                    UserId = _userService.UserId ?? Guid.Empty,
                    ConversationId = ConversationId
                };

                _context.ChatMessages.Add(message);

                return ResultResponse<ChatMessageModel>.Success(
                    message);

            }
            catch (Exception ex)
            {
                return ResultResponse<ChatMessageModel>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.BadRequest,
                        ex.Message));
            }
        }
    }
}
