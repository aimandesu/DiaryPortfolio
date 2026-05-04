using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat.Attachment
{
    public class ChatAttachmentPhotoModel
    {
        required public Guid ChatAttachmentId { get; set; }
        public ChatAttachementModel? ChatAttachment { get; set; }
        required public Guid PhotoModelId { get; set; }
        public PhotoModel? Photo { get; set; }
    }
}
