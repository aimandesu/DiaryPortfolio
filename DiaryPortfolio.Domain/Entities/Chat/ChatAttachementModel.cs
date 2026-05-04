using DiaryPortfolio.Domain.Entities.Chat.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities.Chat
{
    public class ChatAttachementModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        //
        public Guid ChatMessageId { get; set; }
        public ChatMessageModel? ChatMessage { get; set; }
        public List<ChatAttachmentVideoModel> VideosAttachment { get; set; } = [];
        public List<ChatAttachmentPhotoModel> PhotosAttachment { get; set; } = [];
        public List<ChatAttachmentFilesModel> FilesAttachment { get; set; } = [];
    }
}
