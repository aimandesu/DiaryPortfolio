using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class ConditionFaker : Faker<ConditionModel>
    {
        public ConditionFaker()
        {
            RuleFor(c => c.Id, f => Guid.NewGuid());
            RuleFor(c => c.AvailableTime, f => f.Date.Past());
            RuleFor(c => c.DeletedTime, f => f.Random.Bool() ? f.Date.Recent() : (DateTime?)null);
        }
    }
}