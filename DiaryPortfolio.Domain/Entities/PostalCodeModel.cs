using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryPortfolio.Domain.Entities
{
    public class PostalCodeModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PostalNumber { get; set; } = string.Empty;

        public Guid CityId { get; set; }
        public CityModel? City { get; set; }

        public ICollection<LocationModel> Locations { get; set; } = [];
    }
}
