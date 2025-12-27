using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.IAuthenticationRepository
{
    public interface IAuthenticationRepository
    {
        // Authentication Process
        Task<UserModel?> SignUp(UserModel user, string password);
        Task<ResultResponse<UserModel>> Login(string EmailOrUsername, string password);
    }
}
