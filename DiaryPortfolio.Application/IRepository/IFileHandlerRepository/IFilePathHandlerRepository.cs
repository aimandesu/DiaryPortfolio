using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.IFileHandlerRepository
{
    public interface IFilePathHandlerRepository
    {
        string BuildPath(
            MediaType mediaType,
            MediaSubType mediaSubType,
            string fileName
        );
    }
}
