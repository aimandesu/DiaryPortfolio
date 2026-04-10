using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.DiaryProfile.Media.Create;
using DiaryPortfolio.Application.Features.DiaryProfile.Media.Delete;
using DiaryPortfolio.Application.Features.DiaryProfile.Media.GetAll;
using DiaryPortfolio.Application.Features.DiaryProfile.Media.Update;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DiaryPortfolio.Api.Controller
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
        public async Task<ActionResult<ResultResponse<Pagination<MediaModelDto>>>> GetAllMediaByUsername(
            [FromQuery] QuerySearchObject query,
            [FromQuery] string username,
            CancellationToken cancellationToken
        )
        {
            var request = new GetAllMediaRequest(query, username);
            return await _mediator.Send(request, cancellationToken);

        }

        [HttpPost("uploadMedia")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<MediaModelDto>>> UploadMedia(
            [FromForm] string jsonMediaUploadRequest,
            [FromForm] List<IFormFile> files,
            CancellationToken cancellationToken
        )
        {
            var mediaUploadRequest = JsonSerializer.Deserialize<MediaUpload>(
                jsonMediaUploadRequest,
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNameCaseInsensitive = true
                }
            );

            var mediaUpload = new MediaUpload
            {
                Title = mediaUploadRequest?.Title ?? "",
                Description = mediaUploadRequest?.Description ?? "",
                MediaStatus = mediaUploadRequest?.MediaStatus ?? MediaStatus.Public,
                MediaType = mediaUploadRequest?.MediaType ?? MediaType.Post,
                SpaceId = mediaUploadRequest?.SpaceId ?? "",
                Location = mediaUploadRequest?.Location,
                Condition = mediaUploadRequest?.Condition,
                FileStreams = files.Select(f => new MediaStream
                {
                    Stream = f.OpenReadStream(),
                    FileName = f.FileName
                }).ToList(),
            };
            var request = new CreateMediaRequest(mediaUpload);
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpPost("updateMedia/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<MediaModelDto>>> UpdateMedia(
            [FromRoute] string id,
            [FromForm] string jsonMediaUploadRequest,
            [FromForm] List<IFormFile> files,
            CancellationToken cancellationToken
        )
        {
            var mediaUploadRequest = JsonSerializer.Deserialize<MediaUpload>(
                jsonMediaUploadRequest,
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNameCaseInsensitive = true
                }
            );

            var mediaUpload = new MediaUpload
            {
                Title = mediaUploadRequest?.Title ?? "",
                Description = mediaUploadRequest?.Description ?? "",
                MediaStatus = mediaUploadRequest?.MediaStatus ?? MediaStatus.Public,
                MediaType = mediaUploadRequest?.MediaType ?? MediaType.Post,
                SpaceId = mediaUploadRequest?.SpaceId ?? "",
                Location = mediaUploadRequest?.Location,
                Condition = mediaUploadRequest?.Condition,
                FileStreams = files.Select(f => new MediaStream
                {
                    Stream = f.OpenReadStream(),
                    FileName = f.FileName
                }).ToList(),
            };

            var request = new UpdateMediaRequest(id, mediaUpload);
            return await _mediator.Send(request, cancellationToken);
        }

        [HttpDelete("DeleteMedia")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<MediaModel>>> DeleteMedia(
            [FromQuery] string id,
            CancellationToken cancellationToken
        )
        {
            var request = new DeleteMediaRequest(id);
            return await _mediator.Send(request, cancellationToken);
        }

    }

}
