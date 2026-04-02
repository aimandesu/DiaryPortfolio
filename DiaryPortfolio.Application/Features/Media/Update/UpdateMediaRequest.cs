using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Media;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.Update
{
    public sealed record class UpdateMediaRequest(
        string mediaId,
        MediaUpload MediaUpload
    ) : IRequest<ResultResponse<MediaModelDto>>;
}
