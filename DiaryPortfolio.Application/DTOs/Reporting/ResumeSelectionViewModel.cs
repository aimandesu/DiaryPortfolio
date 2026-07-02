using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.DTOs.Reporting;

public class ResumeSelectionViewModel
{
    public string UserId { get; set; } = string.Empty;
    public ResumeReportDto ResumeData { get; set; } = new();
    public List<ResumeTemplateModel> Templates { get; set; } = [];
}