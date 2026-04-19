using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Create
{
    internal class CreateResumeHandler : IRequestHandler<CreateResumeRequest, ResultResponse<ResumeModel>>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public CreateResumeHandler(
           IResumeRepository resumeRepository, 
           IFileHandlerRepository fileHandlerRepository,
           IUnitOfWork unitOfWork,
           IUserService userService)
        {
            _resumeRepository = resumeRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async ValueTask<ResultResponse<ResumeModel>> Handle(
            CreateResumeRequest request, 
            CancellationToken cancellationToken)
        {
            var pdfBytes = await _resumeRepository.GenerateResumeReport(_userService
                .UserId.ToString() ?? "");

            var stream = new MemoryStream(pdfBytes);

            var mediaStream = new MediaStream
            {
                Stream = stream,
                FileName = "ResumeReport.pdf"
            };

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                new List<MediaStream> { mediaStream },
                MediaType.Profile
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<ResumeModel>.Failure(uploadResult.Error);
            }

            var uploadResumeResult = await _resumeRepository.UploadResume(
                templateId: request.TemplateId,
                resume: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.File))
                    .Select(e => e[MediaSubType.File].Files)
                    .ToList().First()
            );

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);
                return ResultResponse<ResumeModel>.Success(uploadResumeResult.Result);
            }
            catch (Exception ex) 
            {
                _fileHandlerRepository.DeleteFiles(
                    [
                    .. uploadResult.Result
                        .Where(e => e.ContainsKey(MediaSubType.File))
                        .Select(e => e[MediaSubType.File].Files?.Url ?? "")
                    ]
                );

                return ResultResponse<ResumeModel>.Failure(
                    new Error(System.Net.HttpStatusCode.Conflict, ex.ToString())
                );

            }

        }
    }
}
