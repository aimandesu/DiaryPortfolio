using DiaryPortfolio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class ResumeModel : IUserOwner, IUserOwnerQuery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? FileId { get; set; }
        public Guid? TemplateId { get; set; }

        //EF
        public FileModel? ResumeFile { get; set; }
        public ResumeTemplateModel? ResumeTemplate { get; set; }

        public Guid PortfolioProfileId { get; set; }
        public PortfolioProfileModel? PortfolioProfile { get; set; }

        public Guid OwnerId => PortfolioProfile?.UserId ?? Guid.Empty;

        public static IQueryable<object> WithOwnerIncludes(DbContext context)
            => context.Set<ResumeModel>()
                .Include(r => r.PortfolioProfile);
    }
}
