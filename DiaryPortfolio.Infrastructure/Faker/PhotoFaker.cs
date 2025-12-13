using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class PhotoFaker : Faker<PhotoModel>
    {
        public PhotoFaker()
        {
            RuleFor(p => p.Id, f => Guid.NewGuid());
            RuleFor(p => p.Url, f => f.Image.PicsumUrl());
            RuleFor(p => p.Mime, f => f.Lorem.Sentence());
            RuleFor(p => p.Width, f => f.Random.Int(100, 4000));
            RuleFor(p => p.Height, f => f.Random.Int(100, 4000));
            RuleFor(p => p.Size, f => f.Random.Int(1000, 5000000));
        }
    }
}