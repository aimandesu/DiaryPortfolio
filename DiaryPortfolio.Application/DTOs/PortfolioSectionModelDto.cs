using System;

namespace DiaryPortfolio.Application.DTOs
{
    public class PortfolioSectionModelDto
    {
        required public Guid Id { get; set; }
        public string SectionType { get; set; } = string.Empty;
        public int? X { get; set; }
        public int? Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }
}
