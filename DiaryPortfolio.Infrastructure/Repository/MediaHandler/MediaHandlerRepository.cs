using Azure.Core;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.DTOs.Condition;
using DiaryPortfolio.Application.DTOs.Location;
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

        public async Task<ResultResponse<MediaModel?>> GetMediaWithFiles(Guid mediaId)
        {
            var response = await _context.Medias
                .Include(m => m.PhotoModels)
                .Include(m => m.VideoModels)
                .FirstOrDefaultAsync(m => m.Id == mediaId);

            return ResultResponse<MediaModel?>.Success(response);
        }

        public async Task<ResultResponse<MediaModel>> UpdateMedia(
            Guid id, 
            MediaUpload media, 
            List<VideoModel> videos, 
            List<PhotoModel> photos)
        {
            try
            {
                var existingMedia = await _context.Medias
                    .Include(m => m.PhotoModels)
                    .Include(m => m.VideoModels)
                    .Include(m => m.LocationModel)
                    .Include(m => m.ConditionModel)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (existingMedia == null)
                {
                    return ResultResponse<MediaModel>.Failure(
                        new Error("NotFound", "Media not found")
                    );
                }

                var textStyle = EnumHelper.ParseEnumOrThrow<TextStyle>(media.TextTitle.ToString());

                var textLookup = _context.TextStyle
                    .Where(e => e.TextStyle.ToString() == textStyle.ToString())
                    .Select(e => e.Id)
                    .FirstOrDefault();

                var spaceIdLookup = _context.Spaces
                    .Where(e => e.Id == new Guid(media.SpaceId))
                    .Select(e => e.Id)
                    .FirstOrDefault();

                var statusSelection = _context.Selections
                    .Where(s => s.Selection == media.MediaStatus.ToString())
                    .Select(s => s.Id)
                    .FirstOrDefault();

                var typeSelection = _context.Selections
                    .Where(s => s.Selection == media.MediaType.ToString())
                    .Select(s => s.Id)
                    .FirstOrDefault();

                existingMedia.Title = media.Title;
                existingMedia.Description = media.Description;
                existingMedia.MediaStatusSelectionId = statusSelection;
                existingMedia.MediaTypeSelectionId = typeSelection;
                existingMedia.TextId = textLookup;
                existingMedia.SpaceId = spaceIdLookup;

                if (existingMedia.LocationModel != null)
                {
                    existingMedia.LocationModel.Name = media.Location?.Name ?? "";
                    existingMedia.LocationModel.Latitude = media.Location?.Latitude ?? "";
                    existingMedia.LocationModel.Longitude = media.Location?.Longitude ?? "";
                }

                if (existingMedia.ConditionModel != null)
                {
                    existingMedia.ConditionModel.AvailableTime = media.Condition?.AvailableTime ?? DateTime.UtcNow;
                    existingMedia.ConditionModel.DeletedTime = media.Condition?.DeletedTime;
                }


                //Photos
                var newPhotoUrls = photos.Select(p => p.Url).ToHashSet();

                var photosToDelete = existingMedia.PhotoModels
                    .Where(p => !newPhotoUrls.Contains(p.Url))
                    .ToList();

                _context.Photos.RemoveRange(photosToDelete);
                existingMedia.PhotoModels.Clear();

                foreach (var photo in photos)
                {
                    photo.MediaId = existingMedia.Id;
                    existingMedia.PhotoModels.Add(photo);
                }


                //Videos
                var newVideoUrls = videos.Select(v => v.Url).ToHashSet();

                var videosToDelete = existingMedia.VideoModels
                    .Where(v => !newVideoUrls.Contains(v.Url))
                    .ToList();

                _context.Videos.RemoveRange(videosToDelete);
                existingMedia.VideoModels.Clear();

                foreach (var video in videos)
                {
                    video.MediaId = existingMedia.Id;
                    existingMedia.VideoModels.Add(video);
                }

                return ResultResponse<MediaModel>.Success(existingMedia);
            }
            catch (Exception ex)
            {
                return ResultResponse<MediaModel>.Failure(
                    new Error("Media_Update_Failed", ex.Message)
                );
            }
        }

        public Task<ResultResponse<MediaModel>> UploadMedia(
           MediaUpload media,
            List<VideoModel> videos,
            List<PhotoModel> photos
            )
        {

            try
            {
               
                var textLookup = _context.TextStyle
                    .Where(e => e.TextStyle.ToString() == media.TextTitle.ToString())
                    .Select(e => e.Id)
                    .FirstOrDefault();

                var spaceIdLookup = _context.Spaces
                    .Where(e => e.Id == new Guid(media.SpaceId))
                    .Select(e => e.Id)
                    .FirstOrDefault();

                var locationLookup = new LocationModel
                {
                    Name = media.Location?.Name ?? "",
                    Latitude = media.Location?.Latitude ?? "",
                    Longitude = media.Location?.Longitude ?? ""
                };

                var conditionLookup = new ConditionModel
                {
                    AvailableTime = media.Condition?.AvailableTime ?? DateTime.UtcNow,
                    DeletedTime = media.Condition?.DeletedTime
                };

                var statusSelection = _context.Selections
                    .Where(s => s.Selection == media.MediaStatus.ToString())
                    .Select(s => s.Id)
                    .FirstOrDefault();

                var typeSelection = _context.Selections
                    .Where(s => s.Selection == media.MediaType.ToString())
                    .Select(s => s.Id)
                    .FirstOrDefault();

                var mediaUpload = new MediaModel
                {
                    Title = media.Title,
                    Description = media.Description,
                    MediaStatusSelectionId = statusSelection,
                    MediaTypeSelectionId = typeSelection,
                    CreatedAt = DateTime.UtcNow,
                    TextId = textLookup,
                    SpaceId = spaceIdLookup,
                    LocationModel = locationLookup,
                    ConditionModel = conditionLookup,
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
