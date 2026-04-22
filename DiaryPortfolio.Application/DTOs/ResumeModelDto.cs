using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.DTOs
{
    public class ResumeModelDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? FileId { get; set; }
        public Guid? TemplateId { get; set; }

        //EF
        public FileModel? ResumeFile { get; set; }
    }
}
