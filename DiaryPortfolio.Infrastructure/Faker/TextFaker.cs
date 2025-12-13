using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class TextFaker : Faker<TextModel>
    {
        public TextFaker()
        {
            RuleFor(t => t.Id, f => Guid.NewGuid());
        }
    }
}