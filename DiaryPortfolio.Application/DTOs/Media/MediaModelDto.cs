using DiaryPortfolio.Application.DTOs.Condition;
using DiaryPortfolio.Application.DTOs.Location;
using DiaryPortfolio.Application.DTOs.Photo;
using DiaryPortfolio.Application.DTOs.Space;
using DiaryPortfolio.Application.DTOs.Video;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs.Media
{
    public class MediaModelDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string MediaStatus { get; set; } = string.Empty;
        public string MediaType { get; set; } = string.Empty;

        //FK, EF 
        // Media Table
        //public Guid SpaceId { get; set; }
        public SpaceModelDto? SpaceModel { get; set; }

        //Location Table
        public LocationModelDto? LocationModel { get; set; }

        //Condition Table
        public ConditionModelDto? ConditionModel { get; set; }

        //Videos
        public List<VideoModel> VideoModels { get; set; } = [];

        //Photos
        public List<PhotoModel> PhotoModels { get; set; } = [];

        //EF
        //public Guid? CollectionId { get; set; }
        public CollectionModel? CollectionModel { get; set; }
    }
}
