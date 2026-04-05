using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.Mapper.Condition;
using DiaryPortfolio.Application.Mapper.Location;
using DiaryPortfolio.Application.Mapper.Space;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper.Media
{
    static internal class MediaModelMapper
    {
        public static MediaModelDto ToMediaModelDto(this MediaModel mediaModel)
        {
            return new MediaModelDto
            {
                Id = mediaModel.Id,
                Title = mediaModel.Title,
                Description = mediaModel.Description,
                MediaStatus = mediaModel.SelectionMediaStatusModel?.Selection ?? "",
                MediaType = mediaModel.SelectionMediaTypeModel?.Selection ?? "",
                CreatedAt = mediaModel.CreatedAt,
                //SpaceId = mediaModel.SpaceId,
                SpaceModel = mediaModel?.SpaceModel?.ToSpaceModelDto(),
                LocationModel = mediaModel?.LocationModel?.ToLocationModelDto(),
                ConditionModel = mediaModel?.ConditionModel?.ToConditionModelDto(),
                VideoModels   = mediaModel?.MediaVideos?.Select(v => v.Video).OfType<VideoModel>().ToList() ?? [],
                PhotoModels = mediaModel?.MediaPhotos?.Select(p => p.Photo).OfType<PhotoModel>().ToList() ?? [],
                //CollectionId = mediaModel?.CollectionId,
                CollectionModel = mediaModel?.CollectionModel
            };
        }
    }
}
