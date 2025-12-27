using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.IRepository.IUserRepository
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserByUsername(string username);
        
    }
}
