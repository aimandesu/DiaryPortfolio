using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository.MediaHandler
{
    public class MediaHandlerRepository : IMediaHandlerRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaHandlerRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> DeleteMedia(string mediaPath)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> GetFile(string mediaUrl)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse<MediaModel>> UploadMedia(
            List<VideoModel> videos, 
            List<PhotoModel> photos)
        {

            try
            {
                var mediaUpload = new MediaModel
                {
                    CreatedAt = DateTime.UtcNow,
                    TextId = Guid.Parse("963A7113-1573-42A4-B44E-1E7F8EA34709")
                };

                foreach (var photo in photos)
                {
                    photo.MediaId = mediaUpload.Id;
                    mediaUpload.PhotoModels.Add(photo);
                }

                foreach (var video in videos)
                {
                    video.MediaId = mediaUpload.Id;
                    mediaUpload.VideoModels.Add(video);
                }

                _context.Medias.Add(mediaUpload);

                return Task.FromResult(ResultResponse<MediaModel>.Success(mediaUpload));

            } 
            catch (Exception ex)
            {
                return Task.FromResult(ResultResponse<MediaModel>.Failure(
                    new Error(
                        Code: "Media_Upload_Failed", 
                        Description: ex.Message
                    )
                ));
            }
           
        }
    }
}
