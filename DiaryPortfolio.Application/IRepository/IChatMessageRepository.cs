using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IChatMessageRepository
    {
        Task<ResultResponse<ChatMessageModel>> CreateMessage(
            MessageUpload messageUpload,
            Guid conversationId);
    }
}
