using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ResumeModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? FileId { get; set; }
        public Guid? TemplateId { get; set; }

        //EF
        public FileModel? ResumeFile { get; set; }
        public ResumeTemplateModel? ResumeTemplate { get; set; }
    }
}
