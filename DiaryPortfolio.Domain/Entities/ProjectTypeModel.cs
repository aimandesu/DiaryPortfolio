using DiaryPortfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ProjectTypeModel : IUserOwner
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid PortfolioProfileId { get; set; }
        public PortfolioProfileModel? PortfolioProfile { get; set; }

        public List<ProjectModel> Projects { get; set; } = [];

        public Guid OwnerId => PortfolioProfile?.UserId ?? Guid.Empty;
    }
}
