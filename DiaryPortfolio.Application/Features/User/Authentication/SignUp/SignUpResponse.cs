using DiaryPortfolio.Application.DTOs.User;
using DiaryPortfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Application.Features.User.Authentication.SignUp
{
    public class SignUpResponse : TokenModel
    {
        public UserModelDto? User { get; set; }
    }
}
