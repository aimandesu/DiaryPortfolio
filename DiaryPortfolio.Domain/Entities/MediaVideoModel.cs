using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class MediaVideoModel
    {
        public Guid MediaModelId { get; set; } = Guid.NewGuid();
        public MediaModel? Media { get; set; }

        public Guid VideoModelId { get; set; } = Guid.NewGuid();
        public VideoModel? Video { get; set; }
    }
}
