using DiaryPortfolio.Application.DTOs.Video;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper.Video
{
    static internal class VideoModelMapper
    {
            public static VideoModelDto ToVideoModelDto(this VideoModel videoModel)
            {
                return new VideoModelDto
                {
                    Id = videoModel.Id,
                    Url = videoModel.Url,
                    Mime = videoModel.Mime,
                    Duration = videoModel.Duration,
                    Size = videoModel.Size
                };
        }
    }
}
