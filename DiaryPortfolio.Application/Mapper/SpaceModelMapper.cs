using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class SpaceModelMapper
    {
        public static SpaceModelDto ToSpaceModelDto(this SpaceModel spaceModel)
        {
            return new SpaceModelDto
            {
                Id = spaceModel.Id,
                Title = spaceModel.Title,
                CreatedAt = spaceModel.CreatedAt,
                UserId = spaceModel.DiaryProfileId
            };
        }
    }
}
