using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class EducationModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Institution { get; set; } = string.Empty;
        public string Achievement { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //FK, EF
        public Guid UserId { get; set; }
        public UserModel? User { get; set; }
        public Guid LocationId { get; set; }
        public LocationModel? Location { get; set; }
        public Guid? FileId { get; set; }
        public FilesModel? EducationFile { get; set; }
        public Guid SelectionId { get; set; }
        public SelectionModel? EducationTier { get; set; }
    }
}
