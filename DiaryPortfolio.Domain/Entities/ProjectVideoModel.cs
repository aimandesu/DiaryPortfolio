using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ProjectVideoModel
    {
        public Guid ProjectModelId { get; set; }
        public ProjectModel? Project { get; set; }

        public Guid VideoModelId { get; set; }
        public VideoModel? Video { get; set; }
    }
}
