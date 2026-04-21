using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Request
{
    public class EducationUpload
    {
        public string Institution { get; set; } = string.Empty;
        public string Achievement { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        required public EducationTierEnum Education { get; set; }
        public LocationModelDto? Location { get; set; }
        public MediaStream? FileStream { get; set; }
    }
}
    