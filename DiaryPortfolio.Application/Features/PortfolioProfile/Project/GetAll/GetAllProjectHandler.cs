using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.Mapper;
using DiaryPortfolio.Infrastructure.Services;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.PortfolioProfile.Project.GetAll
{
    internal class GetAllProjectHandler : IRequestHandler<GetAllProjectRequest, ResultResponse<List<ProjectModelDto>>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectHandler(
            IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async ValueTask<ResultResponse<List<ProjectModelDto>>> Handle(
            GetAllProjectRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _projectRepository
                .GetAllProject(request.Username);

                return ResultResponse<List<ProjectModelDto>>.Success(
                    response.Result.Select(
                        e => e.ToProjectModelDto()).ToList());

            }
            catch (AppException ex)
            {
                return ResultResponse<List<ProjectModelDto>>.Failure(
                   new Error(ex.StatusCode, ex.Message)
                );
            }
        }
    }
}
