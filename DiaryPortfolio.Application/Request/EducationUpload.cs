using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.Request
{
    public class EducationUpload
    {
        public string Institution { get; set; } = string.Empty;
        public string Achievement { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public required EducationTierEnum Education { get; set; }
        public LocationModel? Location{ get; set; }
        [JsonIgnore]
        public MediaStream? EducationFile { get; set; }
    }
}
    