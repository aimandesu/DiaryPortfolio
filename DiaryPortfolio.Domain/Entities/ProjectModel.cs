using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ProjectModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //FK, EF
        public Guid SelectionId { get; set; }
        public SelectionModel? ProjectType { get; set; }
        public Guid UserId { get; set; }
        public UserModel? User { get; set; }
        public Guid? FileId { get; set; }
        public FilesModel? ProjectFile { get; set; }
    }
}
