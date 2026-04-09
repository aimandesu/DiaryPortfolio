using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Request;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;

namespace DiaryPortfolio.Application.IRepository
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserByUsername(
            string username, ProfileType profileType);
        Task<ResultResponse<UserModel>> UploadProfile(
            ProfileUpload profileUpload,
            PhotoModel? profilePhoto,
            FileModel? resumeFile
        );
    }
}
