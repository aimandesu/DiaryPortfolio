using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities.Chat;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ConversationRepository(
            ApplicationDbContext context, 
            IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ResultResponse<ConversationModel>> CreateConversation(
            string conversationEnum,
            List<string> userIds)
        {
            try
            {

                var conversation = new ConversationModel
                {
                    ConversationType = conversationEnum,
                    UserId = _userService.UserId ?? Guid.Empty,
                };

                _context.Conversations.Add(conversation);

                _context.ConversationParticipants.AddRange(
                    userIds.Select(userId => new ConversationParticipantModel
                    {
                        UserId = new Guid(userId),
                        ConversationId = conversation.Id
                    })
                );

                return ResultResponse<ConversationModel>.Success(
                   conversation);

            }
            catch (Exception ex)
            {
                return ResultResponse<ConversationModel>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.BadRequest,
                        ex.Message)
                    );
            }

        }

        public async Task<ResultResponse<ConversationModel?>> GetConversation(
            Guid id)
        {

            try
            {
                var conversation = await _context.Conversations
                    .Include(p => p.ConversationParticipant)
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync();

                return ResultResponse<ConversationModel?>.Success(
                    conversation);
            }
            catch (Exception ex)
            {
                return ResultResponse<ConversationModel?>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.BadRequest,
                        ex.Message)
                    );
            }

        }
    }
}
