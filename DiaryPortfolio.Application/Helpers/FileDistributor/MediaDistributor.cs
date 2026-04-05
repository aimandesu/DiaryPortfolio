using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Helpers.FileDistributor
{
    public class MediaDistributor
    {
        public VideoModel? Videos { get; set; }
        public PhotoModel? Photos { get; set; }
        public FileModel? Files { get; set; }
    }
}
