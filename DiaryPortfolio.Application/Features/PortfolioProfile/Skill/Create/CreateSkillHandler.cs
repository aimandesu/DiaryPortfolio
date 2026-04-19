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

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Skill.Create
{
    internal class CreateSkillHandler : IRequestHandler<CreateSkillRequest, ResultResponse<SkillModelDto>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly ISelectionHelper _selectionHelper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSkillHandler(
            ISkillRepository skillRepository, 
            ISelectionHelper selectionHelper,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _selectionHelper = selectionHelper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<ResultResponse<SkillModelDto>> Handle(
            CreateSkillRequest request, 
            CancellationToken cancellationToken)
        {
           
            try
            {
                var skillSelection = await _selectionHelper.GetSelectionIdAsync(
                    request.SkillLevel,
                    cancellationToken
                );

                var skil = new SkillModel
                {
                    Name = request.SkillName,
                    Description = request.Description,
                    SelectionId = skillSelection,
                    PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty,
                };

                var response = await _skillRepository.Create(skil);

                await _unitOfWork.SaveChanges(cancellationToken);

                return ResultResponse<SkillModelDto>.Success(response.ToSkillModelDto());

            }
            catch (AppException ex)
            {
                return ResultResponse<SkillModelDto>.Failure(
                    new Error(ex.StatusCode, ex.Message)
                );
            }

        }
    }
}
