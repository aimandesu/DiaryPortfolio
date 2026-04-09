using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class CustomUrl
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        //FK
        public Guid PortfolioProfileId { get; set; }
        public PortfolioProfileModel? PortfolioProfile { get; set; }
    }
}
