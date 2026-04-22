using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class ResumeModelMapper
    {
        public static ResumeModelDto ToResumeModelDto(this ResumeModel resumeModel)
        {
            return new ResumeModelDto
            {
                Id = resumeModel.Id,
                FileId = resumeModel.FileId,
                TemplateId = resumeModel.TemplateId,
                ResumeFile = resumeModel.ResumeFile,
            };
        }
    }
}
