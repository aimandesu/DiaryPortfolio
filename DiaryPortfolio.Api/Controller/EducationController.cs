using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.PortfolioProfile.Education.Create;
using DiaryPortfolio.Application.Features.PortfolioProfile.Education.Delete;
using DiaryPortfolio.Application.Request;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/education")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EducationController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<EducationModelDto>>> CreateEducation(
            [FromForm] string jsonEducationUploadRequest,
            [FromForm] IFormFile? educationFile,
            CancellationToken cancellationToken)
        {

            var educationUploadRequest = JsonSerializer.Deserialize<EducationUpload>(
                jsonEducationUploadRequest,
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNameCaseInsensitive = true
                }
            );

            if (educationUploadRequest?.Education == null)
            {
                return ResultResponse<EducationModelDto>.Failure(
                    new Error(
                        System.Net.HttpStatusCode.BadRequest,
                        "Education tier is not given"));
            }

            var educationUpload = new EducationUpload
            {
                Institution = educationUploadRequest?.Institution ?? "",
                Achievement = educationUploadRequest?.Achievement ?? "",
                StartDate = educationUploadRequest?.StartDate,
                EndDate = educationUploadRequest?.EndDate,
                Education = educationUploadRequest.Education,
                Location = educationUploadRequest?.Location,
            };

            if (educationFile != null)
            {
                educationUpload.FileStream = new MediaStream
                {
                    Stream = educationFile.OpenReadStream(),
                    FileName = educationFile.FileName
                };
            }

            var request = new CreateEducationRequest(educationUpload);

            return await _mediator.Send(request, cancellationToken);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<EducationModelDto>>> DeleteEducation(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new DeleteEducationRequest(id),
                cancellationToken);
        }


    }
}
