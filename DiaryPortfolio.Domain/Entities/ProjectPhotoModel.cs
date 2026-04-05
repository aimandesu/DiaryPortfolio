using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ProjectPhotoModel
    {
        public Guid ProjectModelId { get; set; }
        public ProjectModel? Project { get; set; }

        public Guid PhotoModelId { get; set; }
        public PhotoModel? Photo { get; set; }
    }
}
