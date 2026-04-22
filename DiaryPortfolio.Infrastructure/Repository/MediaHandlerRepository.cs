using Azure.Core;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Common.Helpers;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    public class MediaHandlerRepository : IMediaHandlerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ISelectionHelper _selectionHelper;

        public MediaHandlerRepository(
            ApplicationDbContext context,
            IUserService userService,
            ISelectionHelper selectionHelper)
        {
            _context = context;
            _userService = userService;
            _selectionHelper = selectionHelper;
        }

        public List<string> DeleteMedia(string mediaId) 
        {
            var userId = _userService.UserId!.Value;
            var filePaths = new List<string>();

            var media = (from m in _context.Medias
                         join s in _context.Spaces on m.SpaceId equals s.Id
                         where s.DiaryProfile.UserId == userId && m.Id == new Guid(mediaId)
                         select m
                         )
                        .Include(m => m.MediaPhotos)
                            .ThenInclude(mp => mp.Photo)
                        .Include(m => m.MediaVideos)
                            .ThenInclude(mp => mp.Video)
                         .Include(c => c.ConditionModel)
                         .Include(l => l.LocationModel)
                         .FirstOrDefault();

            if (media == null)
            {
                return [];
            }

            if (media?.MediaPhotos != null && media?.MediaVideos != null)
            {
                filePaths.AddRange(media.MediaPhotos.Select(p => p?.Photo?.Url ?? ""));
                filePaths.AddRange(media.MediaVideos.Select(v => v?.Video?.Url ?? ""));
            }

            _context.Medias.Remove(media);

            return filePaths;

        }

        public Task<Stream> StreamMediaFile(string mediaUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse<MediaModel?>> GetMediaWithFiles(Guid mediaId)
        {
            var response = await _context.Medias
                .Include(m => m.MediaPhotos)
                    .ThenInclude(mp => mp.Photo)
                .Include(m => m.MediaVideos)
                    .ThenInclude(mp => mp.Video)
                .Include(m => m.LocationModel)
                .Include(m => m.ConditionModel)
                .Include(m => m.CollectionModel)
                .FirstOrDefaultAsync(m => m.Id == mediaId);

            return ResultResponse<MediaModel?>.Success(response);
        }

        public async Task<ResultResponse<MediaModel>> UpdateMedia(
            MediaUpload media,
            List<VideoModel> videos,
            List<PhotoModel> photos,
            MediaModel? existingMedia)
        {
            try
            {

                if (existingMedia == null)
                {
                    return ResultResponse<MediaModel>.Failure(
                        new Error(System.Net.HttpStatusCode.NotFound, "Media not found")
                    );
                }

                var spaceIdLookup = await _selectionHelper.GetSelectionSpaceId(new Guid(media.SpaceId));

                var statusSelection = await _selectionHelper.GetSelectionIdAsync(media.MediaStatus);

                //var typeSelection = await _selectionHelper.GetSelectionIdAsync(media.MediaType);
                //-> bcs will ganggu the location and doesnt seem logic

                existingMedia.Title = media.Title;
                existingMedia.Description = media.Description;
                existingMedia.MediaStatusSelectionId = statusSelection;
                //SelectionMediaStatusModel =
                //existingMedia.MediaTypeSelectionId = typeSelection;
                //SelectionMediaTypeModel = 
                existingMedia.SpaceId = spaceIdLookup;
                //can include spacemodel also, but you need to change spaceIdLookup
                if (media.Location == null)
                {
                    if (existingMedia.LocationModel != null)
                    {
                        _context.Locations.Remove(existingMedia.LocationModel);
                        existingMedia.LocationModel = null;
                        existingMedia.LocationId = null;
                    }
                }
                else
                {
                    if (existingMedia.LocationModel == null)
                    {
                        existingMedia.LocationModel ??= new LocationModel();
                        _context.Locations.Add(existingMedia.LocationModel);
                    }

                    existingMedia.LocationModel.AddressLine1 = media.Location.Name ?? "";
                    existingMedia.LocationModel.Latitude = Convert.ToDouble(media.Location.Latitude);
                    existingMedia.LocationModel.Longitude = Convert.ToDouble(media.Location.Longitude);
                }

                if (media?.Condition == null)
                {
                    if (existingMedia.ConditionModel != null)
                    {
                        _context.Conditions.Remove(existingMedia.ConditionModel);
                        existingMedia.ConditionModel = null;
                    }
                   
                }
                else
                {
                    if(existingMedia.ConditionModel == null)
                    {
                        existingMedia.ConditionModel ??= new ConditionModel();
                        _context.Conditions.Add(existingMedia.ConditionModel);
                    }

                    existingMedia.ConditionModel.AvailableTime = media?.Condition?.AvailableTime ?? DateTime.UtcNow;
                    existingMedia.ConditionModel.DeletedTime = media?.Condition?.DeletedTime;
                }

                //Photos
                if (media?.DeletedPhotoIds != null && media.DeletedPhotoIds.Count > 0)
                {
                    var deletedPhotoIdSet = media.DeletedPhotoIds
                        .Select(id => Guid.Parse(id))
                        .ToHashSet();

                    var mediaPhotos = existingMedia.MediaPhotos
                        .Where(mp => mp.Photo != null && deletedPhotoIdSet.Contains(mp.Photo.Id))
                        .ToList();

                    foreach (var photo in mediaPhotos)
                    {
                        existingMedia.MediaPhotos.Remove(photo);
                        _context.Photos.Remove(photo.Photo);
                    }
                }

                existingMedia.MediaPhotos.AddRange(
                    photos.Select(photo =>
                    {
                        _context.Photos.Add(photo);
                        return new MediaPhotoModel { Photo = photo };
                    })
                );


                //Videos
                if (media?.DeletedVideoIds != null && media.DeletedVideoIds.Count > 0)
                {
                    var deletedVideoIdSet = media.DeletedVideoIds
                        .Select(id => Guid.Parse(id))
                        .ToHashSet();

                    var mediaVideos = existingMedia.MediaVideos
                        .Where(mv => mv.Video != null && deletedVideoIdSet.Contains(mv.Video.Id))
                        .ToList();

                    foreach (var video in mediaVideos)
                    {
                        existingMedia.MediaVideos.Remove(video);
                        _context.Videos.Remove(video.Video);
                    }
                }

                existingMedia.MediaVideos.AddRange(
                    videos.Select(video =>
                    {
                        _context.Videos.Add(video);
                        return new MediaVideoModel { Video = video };
                    })
                );

                return ResultResponse<MediaModel>.Success(existingMedia);
            }
            catch (Exception ex)
            {
                return ResultResponse<MediaModel>.Failure(
                    new Error(System.Net.HttpStatusCode.Conflict, ex.Message)
                );
            }
        }

        public async Task<ResultResponse<MediaModel>> UploadMedia(
            MediaUpload media,
            List<VideoModel> videos,
            List<PhotoModel> photos)
        {
            try
            {
                var spaceIdLookup = await _selectionHelper.GetSelectionSpaceId(new Guid(media.SpaceId));

                var statusSelection = await _selectionHelper.GetSelectionIdAsync(media.MediaStatus);

                var typeSelection = await _selectionHelper.GetSelectionIdAsync(media.MediaType);

                var location = new LocationModel
                {
                    AddressLine1 = media.Location?.Name ?? "",
                    Latitude = Convert.ToDouble(media.Location?.Latitude),
                    Longitude = Convert.ToDouble(media.Location?.Longitude)
                };


                var mediaUpload = new MediaModel
                {
                    Id = media.Id ?? Guid.NewGuid(),
                    Title = media.Title,
                    Description = media.Description,
                    MediaStatusSelectionId = statusSelection,
                    //SelectionMediaStatusModel =
                    MediaTypeSelectionId = typeSelection,
                    //SelectionMediaTypeModel = 
                    CreatedAt = DateTime.UtcNow,
                    SpaceId = spaceIdLookup,
                    //can include spacemodel also, but you need to change spaceIdLookup
                    LocationId = location.Id,
                    //CollectionId = let collection null dulu
                    LocationModel = location,
                    ConditionModel = new ConditionModel
                    {
                        AvailableTime = media.Condition?.AvailableTime ?? DateTime.UtcNow,
                        DeletedTime = media.Condition?.DeletedTime
                    },
                    MediaPhotos = [.. photos.Select(photo => new MediaPhotoModel { Photo = photo })],
                    MediaVideos = [.. videos.Select(video => new MediaVideoModel { Video = video })]
                };

                _context.Medias.Add(mediaUpload);

                return ResultResponse<MediaModel>.Success(mediaUpload); //if no async Task.FromResult
            }
            catch (Exception ex)
            {
                return ResultResponse<MediaModel>.Failure(
                    new Error(
                        Status: System.Net.HttpStatusCode.Conflict,
                        Description: ex.Message
                    )
                );
            }
        
        }
    }
}
