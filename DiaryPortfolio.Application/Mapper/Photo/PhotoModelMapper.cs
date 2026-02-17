using DiaryPortfolio.Application.DTOs.Photo;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper.Photo
{
    static internal class PhotoModelMapper
    {
        public static PhotoModelDto ToPhotoModelDto(this PhotoModel photoModel)
        {
            return new PhotoModelDto
            {
                Id = photoModel.Id,
                Url = photoModel.Url,
                Mime = photoModel.Mime,
                Width = photoModel.Width,
                Height = photoModel.Height,
                Size = photoModel.Size
            };
        }
    }
}
