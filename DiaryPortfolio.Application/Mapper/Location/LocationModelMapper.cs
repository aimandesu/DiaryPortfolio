using DiaryPortfolio.Application.DTOs.Location;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper.Location
{
    static internal class LocationModelMapper
    {
        public static LocationModelDto ToLocationModelDto(this LocationModel locationModel)
        {
            return new LocationModelDto
            {
                Id = locationModel.Id,
                Name = locationModel.Name,
                Latitude = locationModel.Latitude,
                Longitude = locationModel.Longitude,
            };
        }
    }
}
