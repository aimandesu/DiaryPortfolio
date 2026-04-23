using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IMediaHandlerRepository
    {
        Task<ResultResponse<MediaModel>> UploadMedia(
            MediaUpload mediaUpload,
            List<VideoModel> videos,
            List<PhotoModel> photos
        );
        Task<ResultResponse<MediaModel>> UpdateMedia(
            MediaUpload media,
            List<VideoModel> videos,
            List<PhotoModel> photos,
            MediaModel? existingMedia
        );
        Task<Stream> StreamMediaFile(string mediaUrl);
        Task<ResultResponse<MediaModel>> DeleteMedia(
            Guid mediaId);
        Task<ResultResponse<MediaModel?>> GetMediaWithFiles(
            Guid mediaId);
    }
}
