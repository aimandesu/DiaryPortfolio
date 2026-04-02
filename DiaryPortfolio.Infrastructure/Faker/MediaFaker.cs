using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class MediaFaker : Faker<MediaModel>
    {
        public MediaFaker()
        {
            RuleFor(m => m.Id, f => Guid.NewGuid());
            RuleFor(m => m.Title, f => f.System.FileName());
            RuleFor(m => m.Description, f => f.Lorem.Paragraph());
            //RuleFor(m => m.MediaStatus, f => f.PickRandom<Domain.Enum.MediaStatus>());
            //RuleFor(m => m.MediaType, f => f.PickRandom<Domain.Enum.MediaType>());
            RuleFor(m => m.CreatedAt, f => f.Date.Past());
        }
    }
}