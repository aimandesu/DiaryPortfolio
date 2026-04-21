using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ProjectPhotoModel
    {
        public Guid ProjectModelId { get; set; } = Guid.NewGuid();
        public ProjectModel? Project { get; set; }

        public Guid PhotoModelId { get; set; } = Guid.NewGuid();
        public PhotoModel? Photo { get; set; }
    }
}
