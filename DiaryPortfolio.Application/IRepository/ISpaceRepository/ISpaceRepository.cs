using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.ISpaceRepository
{
    public interface ISpaceRepository
    {
        void AddSpace(SpaceModel space);
    }
}
