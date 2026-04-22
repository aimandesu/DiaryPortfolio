using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Media.Update
{
    [RequireOwnership(typeof(MediaModel))]
    public sealed record class UpdateMediaRequest(
        string Id,
        MediaUpload MediaUpload
    ) : IRequest<ResultResponse<MediaModelDto>>,
        IRequireAuthentication,
        IRequireDiaryProfile;
}
