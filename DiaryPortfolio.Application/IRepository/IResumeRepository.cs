using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IResumeRepository
    {
        Task<byte[]> GenerateResumeReport(string userId);
        Task<ResultResponse<ResumeModel>> UploadResume(
            string templateId,
            FileModel? resume);
        Task<ResultResponse<ResumeModel>> DeleteResume(
            string resumeId);
    }
}
