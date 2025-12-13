using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class CollectionFaker : Faker<CollectionModel>
    {
        public CollectionFaker()
        {
            RuleFor(c => c.Id, f => Guid.NewGuid());
            RuleFor(c => c.Title, f => f.Lorem.Sentence(2));
            RuleFor(c => c.Description, f => f.Lorem.Paragraph());
            RuleFor(c => c.CreatedAt, f => f.Date.Past());
        }
    }
}