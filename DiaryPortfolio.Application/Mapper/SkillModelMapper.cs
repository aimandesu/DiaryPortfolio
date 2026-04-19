using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class SkillModelMapper
    {
        public static SkillModelDto ToSkillModelDto(this SkillModel skillModel)
        {
            return new SkillModelDto
            {
                Id = skillModel.Id,
                Name = skillModel.Name,
                Description = skillModel.Description,
                SelectionId = skillModel.SelectionId,
                Selection = skillModel.Selection,
            };
        }
    }
}
