namespace DiaryPortfolio.Api.Views.PDF
{
    public class ExperienceReportViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public List<ExperienceReportItem> Experiences { get; set; } = [];
    }

    public class ExperienceReportItem
    {
        public string Company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
    }
}
