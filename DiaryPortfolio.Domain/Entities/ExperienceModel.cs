using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ExperienceModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Company { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //FK, EF
        public Guid UserId { get; set; }
        public UserModel? User { get; set; }
        public Guid LocationId { get; set; }
        public LocationModel? Location { get; set; }
    }
}
