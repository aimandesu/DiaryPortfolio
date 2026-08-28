using DiaryPortfolio.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiaryPortfolio.Domain.Entities
{
    public class PortfolioSectionModel : IUserOwner, IUserOwnerQuery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SectionType { get; set; } = string.Empty;

        // Position/size on a 12-column grid, in grid units (not pixels).
        // X/Y are null while the section sits unplaced in the palette -
        // it still exists (and keeps its content) but isn't rendered on the page.
        public int? X { get; set; }
        public int? Y { get; set; }
        public int W { get; set; } = 12;
        public int H { get; set; } = 4;

        //FK
        public Guid PortfolioProfileId { get; set; }
        public PortfolioProfileModel? PortfolioProfile { get; set; }

        public Guid OwnerId => PortfolioProfile?.UserId ?? Guid.Empty;

        public const int GridColumns = 12;

        public static readonly string[] DefaultSectionTypes =
        [
            "about", "photo", "skills", "education", "experience", "projects", "resume"
        ];

        // New profiles start with every section placed full-width, stacked
        // top to bottom - a working page on day one, rearrange from there.
        public static List<PortfolioSectionModel> CreateDefaults() =>
            DefaultSectionTypes
                .Select((type, index) => new PortfolioSectionModel
                {
                    SectionType = type,
                    X = 0,
                    Y = index * 4,
                    W = GridColumns,
                    H = 4,
                })
                .ToList();

        public static IQueryable<object> WithOwnerIncludes(DbContext context)
            => context.Set<PortfolioSectionModel>()
                .Include(s => s.PortfolioProfile);
    }
}
