using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Education.Delete
{
    internal class DeleteEducationHandler : IRequestHandler<DeleteEducationRequest, ResultResponse<EducationModelDto>>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEducationHandler(
            IEducationRepository educationRepository,
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _educationRepository = educationRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<EducationModelDto>> Handle(
            DeleteEducationRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _educationRepository.DeleteEducation(request.Id);

                if (response.Error != Error.None)
                {
                    return ResultResponse<EducationModelDto>.Failure(
                        new Error(
                            response.Error.Status, 
                            response.Error.Description)
                        );
                }

                if (response.Result.EducationFile != null)
                {
                    _fileHandlerRepository.DeleteFile(response.Result.EducationFile.Url);
                }

                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<EducationModelDto>.Success(
                    response.Result.ToEducationModelDto());

            }
            catch (AppException ex)
            {
                return ResultResponse<EducationModelDto>.Failure(
                    new Error(ex.StatusCode, ex.Message));
            }

        }
    }
}
