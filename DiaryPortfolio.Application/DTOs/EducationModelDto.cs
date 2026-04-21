using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs
{
    public class EducationModelDto
    {
        required public Guid Id { get; set; }
        public string Institution { get; set; } = string.Empty;
        public string Achievement { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid LocationId { get; set; }
        public LocationModelDto? Location { get; set; }
        public Guid? FileId { get; set; }
        public FileModel? EducationFile { get; set; }
        public Guid SelectionId { get; set; }
        public SelectionModel? EducationTier { get; set; }
    }
}
