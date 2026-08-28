using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Application.IRepository;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DiaryPortfolio.Infrastructure.Repository
{
    internal class PortfolioSectionRepository : IPortfolioSectionRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioSectionRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultResponse<List<PortfolioSectionModel>>> GetPublicLayout(
            string username)
        {
            try
            {
                var sections = await _context.PortfolioSections
                    .Where(s => s.PortfolioProfile.User.UserName == username && s.X != null)
                    .OrderBy(s => s.Y)
                    .ThenBy(s => s.X)
                    .ToListAsync();

                return ResultResponse<List<PortfolioSectionModel>>.Success(sections);
            }
            catch (Exception ex)
            {
                return ResultResponse<List<PortfolioSectionModel>>.Failure(
                    new Error(HttpStatusCode.UnprocessableContent, ex.Message));
            }
        }

        public async Task<ResultResponse<List<PortfolioSectionModel>>> GetMyLayout(
            Guid portfolioProfileId)
        {
            try
            {
                var sections = await _context.PortfolioSections
                    .Where(s => s.PortfolioProfileId == portfolioProfileId)
                    .ToListAsync();

                // Self-heal: a section type added to DefaultSectionTypes after this
                // profile was created (or signed up before this feature existed)
                // won't have a row yet. Add it unplaced - it shows up in the
                // palette rather than jumping onto an already-arranged page.
                var existingTypes = sections.Select(s => s.SectionType).ToHashSet();
                var missingTypes = PortfolioSectionModel.DefaultSectionTypes
                    .Where(type => !existingTypes.Contains(type));

                foreach (var type in missingTypes)
                {
                    var newSection = new PortfolioSectionModel
                    {
                        SectionType = type,
                        PortfolioProfileId = portfolioProfileId,
                        X = null,
                        Y = null,
                    };

                    _context.PortfolioSections.Add(newSection);
                    sections.Add(newSection);
                }

                return ResultResponse<List<PortfolioSectionModel>>.Success(
                    [.. sections.OrderBy(s => s.Y).ThenBy(s => s.X)]);
            }
            catch (Exception ex)
            {
                return ResultResponse<List<PortfolioSectionModel>>.Failure(
                    new Error(HttpStatusCode.UnprocessableContent, ex.Message));
            }
        }

        public async Task<ResultResponse<List<PortfolioSectionModel>>> SaveLayout(
            Guid portfolioProfileId,
            List<SectionPlacement> placements)
        {
            try
            {
                var sections = await _context.PortfolioSections
                    .Where(s => s.PortfolioProfileId == portfolioProfileId)
                    .ToListAsync();

                // Placements for ids that don't belong to this profile are
                // silently ignored - the caller only ever moves their own sections.
                foreach (var placement in placements)
                {
                    var section = sections.FirstOrDefault(s => s.Id == placement.Id);
                    if (section == null) continue;

                    section.X = placement.X;
                    section.Y = placement.Y;
                    section.W = placement.W;
                    section.H = placement.H;
                }

                return ResultResponse<List<PortfolioSectionModel>>.Success(sections);
            }
            catch (Exception ex)
            {
                return ResultResponse<List<PortfolioSectionModel>>.Failure(
                    new Error(HttpStatusCode.UnprocessableContent, ex.Message));
            }
        }

        public async Task<ResultResponse<PortfolioSectionModel>> UnplaceSection(
            string sectionId)
        {
            var id = new Guid(sectionId);

            var section = await _context.PortfolioSections
                .FirstOrDefaultAsync(s => s.Id == id);

            if (section == null)
            {
                return ResultResponse<PortfolioSectionModel>.Failure(
                    new Error(
                        HttpStatusCode.NotFound,
                        "No section with the id provided found"));
            }

            section.X = null;
            section.Y = null;

            return ResultResponse<PortfolioSectionModel>.Success(section);
        }
    }
}
