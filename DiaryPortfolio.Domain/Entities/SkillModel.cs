using DiaryPortfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class SkillModel  : IUserOwner
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid SelectionId { get; set; }
        public SelectionModel? Selection { get; set; }

        //FK, EF
        public Guid PortfolioProfileId { get; set; }
        public PortfolioProfileModel? PortfolioProfile { get; set; }

        public Guid OwnerId => PortfolioProfile?.UserId ?? Guid.Empty;
    }
}
