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
        private int _counter;

        public SpaceFaker(int startIndex = 0)
        {
            _counter = startIndex;

            RuleFor(s => s.Id, f => Guid.NewGuid());

            RuleFor(s => s.Title, f =>
            {
                var title = $"{f.Lorem.Word()}_{_counter++}";
                return title;
            });

            RuleFor(s => s.CreatedAt, f => f.Date.Past());
        }
    }
}