using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository.IMediaHandlerRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService _userService;

        public MediaHandlerRepository(
            ApplicationDbContext context,
            IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public List<string> DeleteMedia(string mediaId) 
        {
            var userId = _userService.UserId!.Value;

            var media = (from m in _context.Medias
                         join s in _context.Spaces on m.SpaceId equals s.Id
                         where s.UserId == userId && m.Id == new Guid(mediaId)
                         select m
                         )
                         .Include(m => m.PhotoModels)
                         .Include(s => s.VideoModels)
                         .FirstOrDefault();
            
            if (media == null)
            {
                return [];
            }

            var filePaths = new List<string>();

            filePaths.AddRange(media.PhotoModels.Select(p => p.Url));
            filePaths.AddRange(media.VideoModels.Select(v => v.Url));

            _context.Medias.Remove(media);

            return filePaths;

        }

        public Task<Stream> GetFile(string mediaUrl)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse<MediaModel>> UploadMedia(
            string title,
            string description,
            MediaStatus mediaStatus,
            MediaType mediaType,
            string spaceTitle,
            string textStyle,
            List<VideoModel> videos,
            List<PhotoModel> photos
            )
        {

            try
            {
               
                var textId = _context.TextStyle
                    .Where(e => e.TextStyle.ToString() == textStyle)
                    .Select(e => e.Id)
                    .FirstOrDefault();

                var spaceId = _context.Spaces
                    .Where(e => e.Title == spaceTitle)
                    .Select(e => e.Id)
                    .FirstOrDefault();

                var mediaUpload = new MediaModel
                {
                    Title = title,
                    Description = description,
                    MediaStatus = mediaStatus,
                    MediaType = mediaType,
                    CreatedAt = DateTime.UtcNow,
                    TextId = textId,
                    SpaceId = spaceId
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
