using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs
{
    public class SkillModelDto
    {
        required public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        required public Guid SelectionId { get; set; }
        public SelectionModel? Selection{ get; set; }
    }
}
