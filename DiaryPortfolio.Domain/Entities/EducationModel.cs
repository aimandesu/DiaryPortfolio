using DiaryPortfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class EducationModel : IUserOwner
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Institution { get; set; } = string.Empty;
        public string Achievement { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //FK, EF
        public Guid PortfolioProfileId { get; set; }
        public PortfolioProfileModel? PortfolioProfile { get; set; }
        public Guid LocationId { get; set; }
        public LocationModel? Location { get; set; }
        public Guid? FileId { get; set; }
        public FileModel? EducationFile { get; set; }
        public Guid SelectionId { get; set; }
        public SelectionModel? Selection { get; set; }

        public Guid OwnerId => PortfolioProfile?.UserId ?? Guid.Empty;
    }
}
