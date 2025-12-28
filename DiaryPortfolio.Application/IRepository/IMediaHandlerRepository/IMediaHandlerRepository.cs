using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.IMediaHandlerRepository
{
    public interface IMediaHandlerRepository
    {
        Task<ResultResponse<MediaModel>> UploadMedia(
            string title,
            string description,
            MediaStatus mediaStatus,
            MediaType mediaType,
            string spaceTitle,
            string textStyle,
            List<VideoModel> videos,
            List<PhotoModel> photos
        );
        Task<Stream> GetFile(string mediaUrl);
        List<string> DeleteMedia(string mediaId);
    }
}
