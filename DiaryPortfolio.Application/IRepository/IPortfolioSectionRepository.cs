using DiaryPortfolio.Application.Common;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.IRepository
{
    public record class SectionPlacement(Guid Id, int X, int Y, int W, int H);

    public interface IPortfolioSectionRepository
    {
        Task<ResultResponse<List<PortfolioSectionModel>>> GetPublicLayout(
            string username);

        Task<ResultResponse<List<PortfolioSectionModel>>> GetMyLayout(
            Guid portfolioProfileId);

        Task<ResultResponse<List<PortfolioSectionModel>>> SaveLayout(
            Guid portfolioProfileId,
            List<SectionPlacement> placements);

        Task<ResultResponse<PortfolioSectionModel>> UnplaceSection(
            string sectionId);
    }
}
