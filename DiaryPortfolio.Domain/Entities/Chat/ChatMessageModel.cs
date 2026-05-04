using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat
{
    public class ChatMessageModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        //FK + Navigational property
        public Guid UserId { get; set; }
        public UserModel? UserModel { get; set; }
        public Guid ConversationId { get; set; }
        public ConversationModel? Conversation { get; set; }
    }
}
