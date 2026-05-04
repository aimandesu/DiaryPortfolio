using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat
{
    public class MessageReceiptModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ReadAt { get; set; } = DateTime.Now;
        //
        public Guid ChatMessageId { get; set; }
        public ChatMessageModel? ChatMessage { get; set; }
        public Guid UserId { get; set; }
        public UserModel? UserModel { get; set; }
    }
}
