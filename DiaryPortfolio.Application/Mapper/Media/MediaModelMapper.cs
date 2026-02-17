using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Application.Mapper.Condition;
using DiaryPortfolio.Application.Mapper.Location;
using DiaryPortfolio.Application.Mapper.Photo;
using DiaryPortfolio.Application.Mapper.Space;
using DiaryPortfolio.Application.Mapper.Video;
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
                MediaStatus = mediaModel.MediaStatus,
                MediaType = mediaModel.MediaType,
                CreatedAt = mediaModel.CreatedAt,
                SpaceId = mediaModel.SpaceId,
                SpaceModel = mediaModel?.SpaceModel?.ToSpaceModelDto(),
                LocationModel = mediaModel?.LocationModel?.ToLocationModelDto(),
                ConditionModel = mediaModel?.ConditionModel?.ToConditionModelDto(),
                VideoModels = mediaModel?.VideoModels?.Select(video => video.ToVideoModelDto()).ToList() ?? [],
                PhotoModels = mediaModel?.PhotoModels?.Select(photo => photo.ToPhotoModelDto()).ToList() ?? [],
                //TextId = mediaModel?.TextId ?? Guid.NewGuid(),
                TextModel = mediaModel?.TextModel,
                //CollectionId = mediaModel?.CollectionId,
                CollectionModel = mediaModel?.CollectionModel
            };
        }
    }
}
