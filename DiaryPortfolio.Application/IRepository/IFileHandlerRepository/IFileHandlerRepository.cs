using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.Helpers.FileDistributor;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository.IFileHandlerRepository
{
    public interface IFileHandlerRepository
    {
        //distribute files to its location
        Task<ResultResponse<List<Dictionary<MediaSubType, MediaDistributor>>>> DistributeFiles(
            List<MediaStream> fileStreams,
            MediaType mediaType
        );
        MediaMetadata ReadMediaMetadata(
            string filePath, 
            MediaSubType mediaSubType,
            string fileExtension
        );
        void DeleteFiles(
            List<string> tempFilePaths
        );
        void DeleteFile(
            string filePath
        );
    }
}