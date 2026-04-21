using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class EducationModelMapper
    {
        public static EducationModelDto ToEducationModelDto(this EducationModel educationModel)
        {
            return new EducationModelDto
            {
                Id = educationModel.Id,
                Institution = educationModel.Institution,
                Achievement = educationModel.Achievement,
                StartDate = educationModel.StartDate,
                EndDate = educationModel.EndDate,
                LocationId = educationModel.LocationId,
                Location = educationModel?.Location?.ToLocationModelDto(),
                FileId = educationModel?.FileId,
                EducationFile = educationModel?.EducationFile,
                SelectionId = educationModel?.SelectionId ?? Guid.Empty,
                EducationTier = educationModel?.EducationTier,
            };
        }
    }
}
