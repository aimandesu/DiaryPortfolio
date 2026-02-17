using DiaryPortfolio.Application.DTOs.Condition;
using DiaryPortfolio.Application.DTOs.Location;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class MediaUpload
    {

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MediaStatus MediaStatus { get; set; } = MediaStatus.Public;
        public MediaType MediaType { get; set; } = MediaType.Post;
        public required string SpaceId { get; set; }
        public TextStyle TextTitle { get; set; } = TextStyle.TimesNewRoman;
        public LocationModelDto? Location { get; set; }
        public ConditionModelDto? Condition { get; set; }
        //actual files to be uploaded
        public List<MediaStream> FileStreams { get; set; } = [];

    }

    public class MediaStream
    {
        public Stream Stream { get; set; } = Stream.Null; // file content
        public string FileName { get; set; } = string.Empty; // original name for path & extension
    }

}
