using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Application.IServices;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using DiaryPortfolio.Infrastructure.Data;
using DiaryPortfolio.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISelectionHelper _selectionHelper;
        private readonly IUserService _userService;

        public ProjectRepository(
            ApplicationDbContext context,
            ISelectionHelper selectionHelper,
            IUserService userService)
        {
            _context = context;
            _selectionHelper = selectionHelper;
            _userService = userService;
        }

        public async Task<ResultResponse<ProjectModel>> CreateProject(
            ProjectUpload projectUpload, 
            FileModel? file, 
            List<VideoModel> videos, 
            List<PhotoModel> photos)
        {
            try
            {
                var selectionResult = await _selectionHelper.GetSelectionResultAsync(
                FilesEnum.Project);

                var projectFile = new FileModel
                {
                    Url = file?.Url ?? "",
                    SelectionId = selectionResult?.SelectionId ?? Guid.Empty
                };

                var project = new ProjectModel
                {
                    Title = projectUpload.Title,
                    Description = projectUpload.Description,
                    PortfolioProfileId = _userService.PortfolioProfileId ?? Guid.Empty,
                    FileId = projectFile.Id,
                    ProjectFile = projectFile,
                    ProjectPhotos = [.. photos.Select(photo => new ProjectPhotoModel { Photo = photo })],
                    ProjectVideos = [.. videos.Select(video => new ProjectVideoModel { Video = video })],
                };

                _context.Projects.Add(project);

                return ResultResponse<ProjectModel>.Success(project);
            }
            catch (AppException ex)
            {
                return ResultResponse<ProjectModel>.Failure(
                   new Error(ex.StatusCode, ex.Message)
               );
            }

        }

        public Task<ResultResponse<ProjectModel>> DeleteProject(
            string projectId)
        {
            throw new NotImplementedException();
        }
    }
}
