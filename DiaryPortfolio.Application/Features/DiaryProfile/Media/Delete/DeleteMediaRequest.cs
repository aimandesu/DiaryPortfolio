using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.DiaryProfile.Media.Delete
{
    [RequireOwnership(typeof(MediaModel))]
    public sealed record class DeleteMediaRequest(
        string Id
    ) : IRequest<ResultResponse<MediaModel>>,
        IRequireAuthentication,
        IRequireDiaryProfile;
}
