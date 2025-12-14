using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Application.Mapper.User
{
    static internal class UserModelMapper
    {
        public static UserModelDto ToUserModelDto(this UserModel userModel)
        {
            return new UserModelDto
            {
                Name = userModel.Name,
                Email = userModel?.Email ?? "",
                UserName = userModel?.UserName ?? ""
            };
        }
    }
}
