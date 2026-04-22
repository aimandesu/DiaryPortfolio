using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs.Reporting
{
    public class ResumeReportDto
    {
        public UserModelDto? User { get; set; }
        public List<ExperienceModelDto> Experiencs { get; set; } = [];
    }



}
