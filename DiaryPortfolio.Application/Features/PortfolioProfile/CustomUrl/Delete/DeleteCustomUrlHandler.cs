using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.CustomUrl.Delete
{
    internal class DeleteCustomUrlHandler : IRequestHandler<DeleteCustomUrlRequest, ResultResponse<CustomUrlModelDto>>
    {
        private readonly ICustomUrlRepository _customUrlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomUrlHandler(
            ICustomUrlRepository customUrlRepository,
            IUnitOfWork unitOfWork)
        {
            _customUrlRepository = customUrlRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<CustomUrlModelDto>> Handle(
            DeleteCustomUrlRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _customUrlRepository.Delete(
                new Guid(request.Id));

                if (response == null)
                {
                    return ResultResponse<CustomUrlModelDto>.Failure(
                            new Error(
                                System.Net.HttpStatusCode.NotFound,
                                "Custom Url with Guid not found")
                        );
                }

                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<CustomUrlModelDto>.Success(response.ToCustomUrlModelDto());

            }
            catch (AppException ex)
            {
                return ResultResponse<CustomUrlModelDto>.Failure(
                    new Error(ex.StatusCode, ex.Message)
                );
            }

        }
    }
}
