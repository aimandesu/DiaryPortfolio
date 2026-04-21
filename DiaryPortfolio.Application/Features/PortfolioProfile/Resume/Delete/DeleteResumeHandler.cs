using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Resume.Delete
{
    internal class DeleteResumeHandler : IRequestHandler<DeleteResumeRequest, ResultResponse<ResumeModel>>
    {
        private readonly IResumeRepository _resumeRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteResumeHandler(
            IResumeRepository resumeRepository, 
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _resumeRepository = resumeRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }
        public async ValueTask<ResultResponse<ResumeModel>> Handle(
            DeleteResumeRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _resumeRepository.DeleteResume(request.resumeId);

                if (response.Error != Error.None)
                {
                    return response;
                }

                if (response.Result.ResumeFile != null) {
                    _fileHandlerRepository.DeleteFile(response.Result.ResumeFile.Url);
                }

                await _unitOfWork.SaveChanges(cancellationToken);

                return response;

            }
            catch (AppException ex)
            {
                return ResultResponse<ResumeModel>.Failure(
                    new Error(ex.StatusCode, ex.Message));
            }

        }
    }
}
