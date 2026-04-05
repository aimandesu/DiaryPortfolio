using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class PortfolioProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Title { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        //FK
        public Guid? LocationId { get; set; }
        public LocationModel? Location { get; set; }

        public Guid? ResumeId { get; set; }
        public ResumeModel? Resume { get; set; }

        public Guid? ProfilePhotoId { get; set; }
        public PhotoModel? ProfilePhoto { get; set; }
        public Guid UserId { get; set; }  // PK + FK
        public UserModel User { get; set; } = null!;
    }
}
