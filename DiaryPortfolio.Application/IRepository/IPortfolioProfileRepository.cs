using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs.Reporting;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IPortfolioProfileRepository
    {
        Task<ResultResponse<UserModel>> UploadProfile(
            ProfileUpload profileUpload,
            PhotoModel? profilePhoto,
            FileModel? resumeFile,
            UserModel? userModel
        );

        Task<ResultResponse<ResumeReportDto>> GenerateResume(
            string userId);

    }
}
