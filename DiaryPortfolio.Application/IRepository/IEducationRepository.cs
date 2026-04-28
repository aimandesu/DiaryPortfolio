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
    public interface IEducationRepository
    {
        Task<ResultResponse<EducationModel>> CreateEducation(
            EducationUpload educationUpload,
            FileModel? file
        );

        Task<ResultResponse<EducationModel>> DeleteEducation(
            string educationId);

        Task<ResultResponse<List<EducationModel>>> GetAllEducation(
            string userName);

    }
}
