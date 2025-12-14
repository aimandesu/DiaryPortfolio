using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.Filter;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.IMediaRepository
{
    public interface IMediaRepository
    {
        Task<Pagination<MediaModel>> GetAllMediaByUsername(QuerySearchObject querySearchObject, Guid userId);
    }
}
