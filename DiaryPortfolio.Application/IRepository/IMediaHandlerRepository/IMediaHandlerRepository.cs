using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
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
            List<VideoModel> videos,
            List<PhotoModel> photos
        );
        Task<Stream> GetFile(string mediaUrl);
        Task<bool> DeleteMedia(string mediaPath);
    }
}
