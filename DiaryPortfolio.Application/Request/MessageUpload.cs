using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Request
{
    public class MessageUpload
    {
        public Guid? ConversationId { get; set; }
        public List<string> UserIds { get; set; } = []; //for creating conversations
        public ConversationEnum ConversationEnum { get; set; } = ConversationEnum.DM;
        public string Message { get; set; } = string.Empty;
    }
}
