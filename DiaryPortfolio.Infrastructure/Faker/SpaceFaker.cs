using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class SpaceFaker : Faker<SpaceModel>
    {
        public SpaceFaker()
        {
            RuleFor(s => s.Id, f => Guid.NewGuid());
            RuleFor(s => s.Title, f => f.Lorem.Word());
            RuleFor(s => s.CreatedAt, f => f.Date.Past());
        }
    }
}