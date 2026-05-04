using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat
{
    public class ConversationInfoModel
    {
        required public Guid ConversationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //
        public Guid? ConversationPhotoId { get; set; } = Guid.Empty;
        public PhotoModel? ConversationPhoto { get; set; }
        public ConversationModel Conversation { get; set; } = null!;
    }
}
