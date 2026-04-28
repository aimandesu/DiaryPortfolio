using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public interface ISkillRepository : IBaseRepository<SkillModel>
    {
        Task<ResultResponse<List<SkillModel>>> GetAllSkill(
            string username);
    }
}
