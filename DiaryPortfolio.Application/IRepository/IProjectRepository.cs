using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IProjectRepository
    {
        Task<ResultResponse<ProjectModel>> CreateProject(
            ProjectUpload projectUpload,
            FileModel? file,
            List<VideoModel> videos,
            List<PhotoModel> photos
        );

        Task<ResultResponse<ProjectModel>> DeleteProject(
            string projectId);


    }
}
