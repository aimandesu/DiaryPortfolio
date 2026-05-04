using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat
{
    public class ConversationModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        required public string ConversationType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //FK + Navigation
        public Guid UserId { get; set; }
        public UserModel? UserModel { get; set; }
        public ConversationInfoModel? ConversationInfo { get; set; }
    }
}
