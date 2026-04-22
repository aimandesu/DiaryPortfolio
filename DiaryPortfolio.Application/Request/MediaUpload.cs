using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Request
{
    public class MediaUpload
    {
        public Guid? Id { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MediaStatus MediaStatus { get; set; } = MediaStatus.Public;
        public MediaType MediaType { get; set; } = MediaType.Post;
        public required string SpaceId { get; set; }
        public LocationModelDto? Location { get; set; }
        public ConditionModelDto? Condition { get; set; }
        //actual files to be uploaded
        public List<MediaStream> FileStreams { get; set; } = [];

        //update
        public List<string> DeletedPhotoIds { get; set; } = [];
        public List<string> DeletedVideoIds { get; set; } = [];

    }

}
