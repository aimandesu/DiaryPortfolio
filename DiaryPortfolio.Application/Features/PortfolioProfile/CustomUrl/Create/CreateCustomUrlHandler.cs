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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.CustomUrl.Create
{
    internal class CreateCustomUrlHandler : IRequestHandler<CreateCustomUrlRequest, ResultResponse<CustomUrlModelDto>>
    {
        private readonly ICustomUrlRepository _customUrlRepository;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomUrlHandler(
            ICustomUrlRepository customUrlRepository,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _customUrlRepository = customUrlRepository;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<CustomUrlModelDto>> Handle(
            CreateCustomUrlRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var customUrl = new CustomUrlModel
                {
                    Name = request.Name,
                    Url = request.Url,
                    PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty
                };

                var respose = await _customUrlRepository.Create(customUrl);

                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<CustomUrlModelDto>.Success(respose.ToCustomUrlModelDto());

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
