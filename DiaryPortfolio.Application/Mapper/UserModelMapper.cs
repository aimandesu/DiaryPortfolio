using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.DTOs;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.Mapper
{
    static internal class UserModelMapper
    {
        public static UserModelDto ToPortfolioProfileDto(this UserModel userModel)
        {
            return new UserModelDto
            {
                Name = userModel?.PortfolioProfile?.Name ?? "",
                Email = userModel?.Email ?? "",
                UserName = userModel?.UserName ?? "",
                Age = userModel?.PortfolioProfile?.Age,
                Title = userModel?.PortfolioProfile?.Title ?? "",
                About = userModel?.PortfolioProfile?.About ?? "",
                Address = userModel?.PortfolioProfile?.Address ?? "",
                Location = userModel?.PortfolioProfile?.Location,
                Resume = userModel?.PortfolioProfile?.Resume,
                ProfilePhoto = userModel?.PortfolioProfile?.ProfilePhoto,
            };
        }

        public static UserModelDto ToDiaryProfileDto(this UserModel userModel)
        {
            return new UserModelDto // ni kene tukar also meh, ikut model dto sendiri, or wel just return je DiaryProfile but diaryProfile kene ada field sendiri la
            {
                Name = userModel?.PortfolioProfile?.Name ?? "",
                Email = userModel?.Email ?? "",
                UserName = userModel?.UserName ?? "",
            };
        }

    }
}
