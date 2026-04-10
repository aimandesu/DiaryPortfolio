using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class ExperienceModelMapper
    {
        public static ExperienceModelDto ToExperienceModelDto(this ExperienceModel conditionModel)
        {
            return new ExperienceModelDto
            {
                Id = conditionModel.Id,
                Company = conditionModel.Company,
                Role = conditionModel.Role,
                Description = conditionModel.Description,
                StartDate = conditionModel.StartDate,
                EndDate = conditionModel.EndDate,
                LocationId = conditionModel.LocationId,
                Location = conditionModel.Location
            };
        }
    }
}
