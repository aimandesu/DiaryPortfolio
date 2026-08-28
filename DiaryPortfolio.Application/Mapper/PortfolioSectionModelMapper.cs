using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class PortfolioSectionModelMapper
    {
        public static PortfolioSectionModelDto ToPortfolioSectionModelDto(this PortfolioSectionModel model)
        {
            return new PortfolioSectionModelDto
            {
                Id = model.Id,
                SectionType = model.SectionType,
                X = model.X,
                Y = model.Y,
                W = model.W,
                H = model.H,
            };
        }
    }
}
