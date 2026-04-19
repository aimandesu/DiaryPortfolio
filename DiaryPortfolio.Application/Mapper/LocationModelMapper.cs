using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class LocationModelMapper
    {
        public static LocationModelDto ToLocationModelDto(this LocationModel locationModel)
        {
            return new LocationModelDto
            {
                Id = locationModel.Id,
                Name = locationModel.AddressLine1,
                Latitude = locationModel.Latitude,
                Longitude = locationModel.Longitude,
            };
        }
    }
}
