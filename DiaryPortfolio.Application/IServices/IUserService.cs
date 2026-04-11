using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IServices
{
    public interface IUserService
    {
        Guid? UserId { get; }
        //string? UserName { get; }
        //string? Email { get; }
        Guid? PortfolioProfileId { get; }
        Guid? DiaryProfileId { get; }
        public bool IsAuthenticated { get; }
    }
}
