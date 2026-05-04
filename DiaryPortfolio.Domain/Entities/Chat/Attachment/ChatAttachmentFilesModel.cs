using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat.Attachment
{
    public class ChatAttachmentFilesModel
    {
        required public Guid ChatAttachmentId { get; set; }
        public ChatAttachementModel? ChatAttachment { get; set; }
        required public Guid FileId { get; set; }
        public FileModel? File { get; set; }
    }
}
