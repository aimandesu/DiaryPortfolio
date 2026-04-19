using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class CustomUrlModelMapper
    {
        public static CustomUrlModelDto ToCustomUrlModelDto(this CustomUrlModel dto)
        {
            return new CustomUrlModelDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Url = dto.Url,
            };
        }
    }
}
