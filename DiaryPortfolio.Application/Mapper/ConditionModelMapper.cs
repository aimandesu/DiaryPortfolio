using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class ConditionModelMapper
    {
        public static ConditionModelDto ToConditionModelDto(this ConditionModel conditionModel)
        {
            return new ConditionModelDto
            {
                Id = conditionModel.Id,
                AvailableTime = conditionModel.AvailableTime,
                DeletedTime = conditionModel.DeletedTime
            };
        }
    }
}
