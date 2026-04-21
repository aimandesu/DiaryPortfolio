using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Common
{
    public class DistributedMediaResult
    {
        public List<VideoModel> Videos { get; set; } = [];
        public List<PhotoModel> Photos { get; set; } = [];
        public List<FileModel> Files { get; set; } = [];
    }
}
