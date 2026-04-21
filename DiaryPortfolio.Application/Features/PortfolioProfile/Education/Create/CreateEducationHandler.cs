using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Education.Create
{
    internal class CreateEducationHandler : IRequestHandler<CreateEducationRequest, ResultResponse<EducationModelDto>>
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IFileHandlerRepository _fileHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEducationHandler(
            IEducationRepository educationRepository, 
            IFileHandlerRepository fileHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _educationRepository = educationRepository;
            _fileHandlerRepository = fileHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<EducationModelDto>> Handle(
            CreateEducationRequest request, 
            CancellationToken cancellationToken)
        {
            var streams = new List<MediaStream>();

            if (request?.EducationUpload?.FileStream is not null)
            {
                streams.Add(request.EducationUpload.FileStream);
            }    

            var uploadResult = await _fileHandlerRepository.DistributeFiles(
                streams,
                MediaType.Education
            );

            if (uploadResult.Error != Error.None)
            {
                return ResultResponse<EducationModelDto>.Failure(uploadResult.Error);
            }

            var uploadFileEducationResult = await _educationRepository.CreateEducation(
                educationUpload: request.EducationUpload,
                file: uploadResult.Result
                    .Where(e => e.ContainsKey(MediaSubType.File))
                    .Select(e => e[MediaSubType.File].Files)
                    .ToList().First()
            );

            try
            {
                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<EducationModelDto>.Success(
                    uploadFileEducationResult.Result.ToEducationModelDto());
            }
            catch (DbUpdateException ex)
            {
                _fileHandlerRepository.DeleteFiles(
                    [
                    .. uploadResult.Result
                        .Where(e => e.ContainsKey(MediaSubType.File))
                        .Select(e => e[MediaSubType.File].Files?.Url ?? "")
                    ]
                );

                return ResultResponse<EducationModelDto>.Failure(
                   new Error(System.Net.HttpStatusCode.Conflict, ex.ToString())
               );
            }

        }
    }
}
