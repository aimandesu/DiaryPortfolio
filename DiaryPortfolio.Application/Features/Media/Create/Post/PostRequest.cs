using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.Media.Create.Post
{
    public sealed record class PostRequest(
        MediaUpload MediaUpload) : IRequest<ResultResponse<MediaModel>>;
}
