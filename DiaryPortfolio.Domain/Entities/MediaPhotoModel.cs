using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class MediaPhotoModel
    {
        public Guid MediaModelId { get; set; } = Guid.NewGuid();
        public MediaModel? Media { get; set; }

        public Guid PhotoModelId { get; set; } = Guid.NewGuid();
        public PhotoModel? Photo { get; set; }
    }
}
