using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Features.Media.Create.Post;
using DiaryPortfolio.Application.Features.Media.GetAll;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DiaryPortfolio.Api.Controller.Media
{
    [Route("api/media")]
    public class MediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediaController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<ResultResponse<Pagination<MediaModel>>>> GetAllMediaByUsername(
            [FromQuery] QuerySearchObject query,
            [FromQuery] string username,
            CancellationToken cancellationToken
        )
        {
            var request = new GetAllMediaRequest(query, username);
            return await _mediator.Send(request, cancellationToken);

        }

        [HttpPost("uploadMedia")]
        public async Task<ActionResult<ResultResponse<MediaModel>>> UploadMedia(
            [FromForm] MediaUpload mediaUploadRequest,
            [FromForm] List<IFormFile> files,
            CancellationToken cancellationToken
        )
        {
            var streams = files.Select(f => f.OpenReadStream()).ToList();

            var mediaUpload = new MediaUpload
            {
                Title = mediaUploadRequest.Title,
                Description = mediaUploadRequest.Description,
                MediaStatus = mediaUploadRequest.MediaStatus,
                MediaType = mediaUploadRequest.MediaType,
                SpaceId = mediaUploadRequest.SpaceId,
                //TextId = mediaUploadRequest.TextId,
                FileStreams = files.Select(f => new MediaStream
                {
                    Stream = f.OpenReadStream(),
                    FileName = f.FileName
                }).ToList(),
            };
            var request = new PostRequest(mediaUpload);
            return await _mediator.Send(request, cancellationToken);
        }

    }

}
