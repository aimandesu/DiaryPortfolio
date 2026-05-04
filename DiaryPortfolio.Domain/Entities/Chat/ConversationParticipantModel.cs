using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat
{
    public class ConversationParticipantModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime JoinedAt { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; }
        //
        public Guid UserId { get; set; }
        public UserModel? UserModel { get; set; }
        public Guid ConversationId { get; set; }
        public ConversationModel? Conversation { get; set; }

    }
}
