using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Condition;
using DiaryPortfolio.Application.DTOs.Location;
using DiaryPortfolio.Application.Request;
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
            MediaUpload mediaUpload,
            List<VideoModel> videos,
            List<PhotoModel> photos
        );
        Task<ResultResponse<MediaModel>> UpdateMedia(
            Guid id,
            MediaUpload media,
            List<VideoModel> videos,
            List<PhotoModel> photos
        );
        Task<Stream> StreamMediaFile(string mediaUrl);
        List<string> GetMediaFiles(string mediaId);
        Task<ResultResponse<MediaModel?>> GetMediaWithFiles(
            Guid mediaId
        );
    }
}
