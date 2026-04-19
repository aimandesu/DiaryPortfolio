using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DiaryPortfolio.Domain.Entities;

namespace DiaryPortfolio.Infrastructure.Faker
{
    public class LocationFaker : Faker<LocationModel>
    {
        public LocationFaker()
        {
            RuleFor(l => l.Id, f => Guid.NewGuid());
            RuleFor(l => l.AddressLine1, f => f.Lorem.Sentence(2));
            RuleFor(l => l.Latitude, f => Convert.ToDecimal(f.Address.Latitude()));
            RuleFor(l => l.Longitude, f => Convert.ToDecimal(f.Address.Longitude()));
        }
    }
}