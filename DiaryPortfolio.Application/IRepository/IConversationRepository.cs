using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities.Chat;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IConversationRepository
    {
        Task<ResultResponse<ConversationModel?>> GetConversation(
            Guid id);
        Task<ResultResponse<ConversationModel>> CreateConversation(
            string conversationEnum,
            List<string> userIds);
    }
}
