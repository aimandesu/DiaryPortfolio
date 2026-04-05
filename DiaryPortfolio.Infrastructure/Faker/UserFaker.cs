using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class UserFaker : Faker<UserModel>
    {
        public UserFaker()
        {
            RuleFor(u => u.Id, f => Guid.NewGuid());
            RuleFor(u => u.UserName, f => f.Internet.UserName());
            //RuleFor(u => u.Name, f => f.Name.FullName());
            RuleFor(u => u.Email, f => f.Internet.Email());
            RuleFor(u => u.PasswordHash, f => f.Internet.Password());
        }
    }
}