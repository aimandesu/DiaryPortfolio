using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs
{
    public class ConditionModelDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public DateTime AvailableTime { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedTime { get; set; }
    }
}
