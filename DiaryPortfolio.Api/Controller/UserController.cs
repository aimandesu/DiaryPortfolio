using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.Features.Reporting.Experience;
using DiaryPortfolio.Application.Features.Reporting.Resume;

using DiaryPortfolio.Application.Features.User.Get;
using DiaryPortfolio.Application.Features.User.Profile.Update;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiaryPortfolio.Api.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator,
            SignInManager<UserModel> signInManager
        )
        {
            _mediator = mediator;
        }


        [HttpGet("{username}/diary")]
        public async Task<ActionResult<ResultResponse<UserModelDto>>> GetUserDiary(
        string username,
        CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new GetUserRequest(username, ProfileType.Diary),
                cancellationToken);
        }

        [HttpGet("{username}/portfolio")]
        public async Task<ActionResult<ResultResponse<UserModelDto>>> GetUserPortfolio(
            string username,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(
                new GetUserRequest(username, ProfileType.Portfolio),
                cancellationToken);
        }

        [HttpPost("updateProfile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<ResultResponse<UserModelDto>>> UpdateProfile(
            [FromForm] string jsonProfileUploadRequest,
            [FromForm] IFormFile? profilePhoto,
            [FromForm] IFormFile? profileResume,
            CancellationToken cancellationToken
        )
        {
            var profileUploadRequest = JsonSerializer.Deserialize<ProfileUpload>(
                jsonProfileUploadRequest,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            var profileUpload = new ProfileUpload
            {
                Name = profileUploadRequest?.Name ?? "",
                Email = profileUploadRequest?.Email ?? "",
                UserName = profileUploadRequest?.UserName ?? "",
                Age = profileUploadRequest?.Age ?? 0,
                Title = profileUploadRequest?.Title ?? "",
                About = profileUploadRequest?.About ?? "",
                Address = profileUploadRequest?.Address ?? "",
                Location = profileUploadRequest?.Location,
            };

            if (profilePhoto != null)
            {
                profileUpload.ProfileFileSteam = new MediaStream
                {
                    Stream = profilePhoto.OpenReadStream(),
                    FileName = profilePhoto.FileName
                };
            }

            if (profileResume != null)
            {
                profileUpload.ResumeFileStream = new MediaStream
                {
                    Stream = profileResume.OpenReadStream(),
                    FileName = profileResume.FileName
                };
            }

            var request = new UpdateProfileRequest(profileUpload);
            return await _mediator.Send(request, cancellationToken);

        }

        [HttpGet("resume/pdf/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ExportPdf(
            [FromRoute] string id,
            CancellationToken cancellationToken)
        {

            var pdfBytes = await _mediator.Send(
                new CreateResumeReportRequest(id),
                cancellationToken
            );

            return File(pdfBytes, "application/pdf", "resume-report.pdf");
        }

    }
}
