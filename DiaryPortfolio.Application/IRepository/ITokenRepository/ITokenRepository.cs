using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.ITokenRepository
{
    public interface ITokenRepository
    {
        TokenModel GenerateToken(string Email, Guid UserId);
    }
}
