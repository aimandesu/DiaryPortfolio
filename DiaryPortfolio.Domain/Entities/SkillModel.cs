using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class SkillModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid SelectionId { get; set; }
        public SelectionModel? SkillLevel { get; set; }

        //FK, EF
        public Guid UserId { get; set; }
        public UserModel? User { get; set; }
    }
}
